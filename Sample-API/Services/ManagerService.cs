using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleApi.Data.Contracts;
using SampleApi.Data.DTO.Request;
using SampleApi.Data.DTO.Response;
using SampleApi.Data.Entities;
using SampleApi.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Services
{
    public class ManagerService : IManagerService
    {
        private readonly AppSettings _appSettings;
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ManagerService(IOptions<AppSettings> appSettings, IUsersRepository usersRepository, IPasswordHasher passwordHasher)
        {
            _appSettings = appSettings.Value;
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> GetById(string id)
        {
            return await _usersRepository.GetUserById(id);
        }

        public async Task<User> AddUser(UserRequest usuarioRequest)
        {
            User user = new()
            {
                ClientId = usuarioRequest.ClientId,
                Name = usuarioRequest.Name,
                LastName = usuarioRequest.LastName,
                UserName = usuarioRequest.UserName,
                TaxPayerNumber = usuarioRequest.TaxPayerNumber,
                Email = usuarioRequest.Email,
                Role = usuarioRequest.Role,
                Password = _passwordHasher.Hash(usuarioRequest.Password),
                Agreements = new List<string>(),
                RefreshTokens = new List<RefreshToken>()
            };

            return await _usersRepository.AddUser(user);
        }


        public async Task<Client> AddClient(ClientRequest clienteRequest)
        {
            Client cliente = new()
            {
                CompanyName = clienteRequest.CompanyName,
                TaxPayerNumber = clienteRequest.TaxPayerNumber,
                Email = clienteRequest.Email,
                Address = clienteRequest.Address,
                PaymentType = clienteRequest.PaymentType,
                BillingPlan = clienteRequest.BillingPlan
            };

            return await _usersRepository.AddClient(cliente);
        }

        public async Task<Role> AddRole(RoleRequest roleRequest)
        {
            Role role = new()
            {
                Name = roleRequest.Name,
                Routes = roleRequest.Routes
            };

            return await _usersRepository.AddRole(role);
        }

        public async Task<OperationsAgreement> UpdateAgreement(AgreementRequest agreementRequest)
        {
            OperationsAgreement operationsAgreement = new()
            {
                Agreement = agreementRequest.Name.ToLower(),
                Module = agreementRequest.Module,
                Operation = agreementRequest.Operation.ToLower(),
                Fields = agreementRequest.Fields,
                Parameters = agreementRequest.Parameters
            };

            try
            {
                if (await _usersRepository.UpdateAgreement(operationsAgreement))
                {
                    await _usersRepository.DeleteOperationsAgreement(operationsAgreement.Agreement);
                    foreach (var item in agreementRequest.Parameters)
                    {
                        await _usersRepository.AddParametersAgreement(new ParametersAgreement() { Agreement = operationsAgreement.Agreement, Parameter = item.Parameter, Value = item.Value });
                    }
                    return operationsAgreement;
                }
                throw new ApiException($"No agreement found for the name {operationsAgreement.Agreement}", 412);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Unexpected failure to change agreement details {ex.Message}");
            }
        }


        public async Task<OperationsAgreement> RegisterAgreement(AgreementRequest cadastroConvenioRequest)
        {

            OperationsAgreement conveniosOperacoes = new()
            {
                Agreement = cadastroConvenioRequest.Name.ToLower(),
                Module = cadastroConvenioRequest.Module,
                Operation = cadastroConvenioRequest.Operation.ToLower(),
                Fields = cadastroConvenioRequest.Fields,
                Parameters = cadastroConvenioRequest.Parameters
            };

            foreach (var item in cadastroConvenioRequest.Parameters)
            {
                await _usersRepository.AddParametersAgreement(new ParametersAgreement() { Agreement = conveniosOperacoes.Agreement, Parameter = item.Parameter, Value = item.Value });
            }

            return await _usersRepository.AddAgreement(conveniosOperacoes);
        }

        public async Task<string> EnableAgreement(EnableAgreementRequest habilitarConvenioRequest)
        {
            return await _usersRepository.EnableAgreement(await _usersRepository.GetUserById(habilitarConvenioRequest.UserId), habilitarConvenioRequest.AgreementName);
        }

        // helper methods

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim(ClaimTypes.Name, user.Name), new Claim(ClaimTypes.Role, user.Role) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var user = await _usersRepository.GetUser(model.UserName);

            // return null if user not found
            if (user == null) return null;

            if (!_passwordHasher.Check(user.Password, model.Password))
                throw new ApiException("Please verify password.");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(ipAddress);

            // save refresh token
            user.RefreshTokens.Add(refreshToken);
            await _usersRepository.UpdateUser(user);

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var user = await _usersRepository.GetUserRefreshToken(token);

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            await _usersRepository.UpdateUser(user);

            // generate new jwt
            var jwtToken = GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = await _usersRepository.GetUserRefreshToken(token);

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            await _usersRepository.UpdateUser(user);

            return true;
        }

        public async Task<string> AddConnectedServices(ConnectedServicesRequest adicionarServicosRequest)
        {
            try
            {
                await _usersRepository.AddConnectedService(adicionarServicosRequest);
            }
            catch (Exception ex)
            {
               throw new ApiException($"Failed to add connected service, details {ex.Message}", 412);
            }

            return "success";
        }

        public async Task<string> AddParametersUserRobot(ParametersUserRobotRequest parametroUsuarioRoboRequest)
        {
            try
            {
                await _usersRepository.AddParametersUserRobot(parametroUsuarioRoboRequest);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Failed to include, details {ex.Message}", 412);
            }

            return "success";
        }

        // helper methods

        private static RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }


    }
}