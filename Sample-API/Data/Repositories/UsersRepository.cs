using SampleApi.Data.Contracts;
using SampleApi.Data.DTO.Request;
using SampleApi.Data.Entities;
using EnumUtils;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Data.Repositories
{
    public class UsersRepository : MongoDbFactoryBase, IUsersRepository
    {

        public UsersRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<User> AddUser(User user)
        {
            return await InsertOneAsync(user);
        }
        public async Task<User> GetUser(string user)
        {
            return await FindOneAsync<User>(x => x.UserName == user);
        }

        public async Task UpdateUser(User user)
        {
            await ReplaceOneAsync(user, user.Id);
        }

        public async Task<User> GetUserRefreshToken(string token)
        {
            return await FindOneAsync<User>(x => x.RefreshTokens.Any(t => t.Token == token));
        }

        public async Task<User> GetUserById(string id)
        {
            return await FindByIdAsync<User>(id);
        }

        public async Task<Client> AddClient(Client client)
        {
            return await InsertOneAsync(client);
        }

   

        public async Task<Role> AddRole(Role role)
        {
            return await InsertOneAsync(role);
        }

        public async Task<OperationsAgreement> AddAgreement(OperationsAgreement operationsAgreement)
        {
            return await InsertOneAsync(operationsAgreement);
        }

        public async Task<bool> UpdateAgreement(OperationsAgreement operationsAgreement)
        {
            var oldAgreement = await FindOneAsync<OperationsAgreement>(x => x.Agreement == operationsAgreement.Agreement);
            if (oldAgreement != null)
            {
                operationsAgreement.Id = oldAgreement.Id;
                await ReplaceOneAsync(operationsAgreement, oldAgreement.Id);
                return true;
            }
            return false;
        }

        public async Task DeleteOperationsAgreement(string agreement)
        {
            await DeleteManyAsync<ParametersAgreement>(x => x.Agreement == agreement);
        }

        public async Task<ParametersAgreement> AddParametersAgreement(ParametersAgreement parametersAgreement)
        {
            return await InsertOneAsync(parametersAgreement);
        }

        public async Task<string> EnableAgreement(User user, string agreement)
        {
            string response = "Agreement already enabled";
            if (!user.Agreements.Contains(agreement))
            {
                user.Agreements.Add(agreement);
                response = "Agreement added with success!";
            }
            await ReplaceOneAsync(user, user.Id);
            return response;
        }

        public async Task AddConnectedService(ConnectedServicesRequest conectedServicesRequest)
        {
            var conectedServices = new ConnectedServices
            {
                Name = conectedServicesRequest.Name,
                Url = conectedServicesRequest.Url,
                Type = conectedServicesRequest.Type,
                Status = conectedServicesRequest.Status,
                UseProxy = conectedServicesRequest.UseProxy,
                VerbHttp = EnumHelper.TryParse<Method>(conectedServicesRequest.VerbHttp, true),
                UserProxy = conectedServicesRequest.UserProxy,
                PasswordProxy = conectedServicesRequest.PasswordProxy
            };

            await InsertOneAsync(conectedServices);
        }

        public async Task<List<ConnectedServices>> GetConnectedServices(string kind)
        {
            return await FindAllAsync<ConnectedServices>(x => x.Type == kind);
        }

        public async Task AddParametersUserRobot(ParametersUserRobotRequest parametersUserRobotRequest)
        {
            var parametersUserRobot = new ParametersUserRobot
            {
                Agreement = parametersUserRobotRequest.Agreement,
                Group = parametersUserRobotRequest.Group,
                Data = parametersUserRobotRequest.Data
            };

            await InsertOneAsync(parametersUserRobot);
        }
    }
}
