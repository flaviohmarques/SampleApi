using SampleApi.Data.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoWrapper.Wrappers;
using System.Threading.Tasks;
using SampleApi.Data.DTO.Response;
using SampleApi.Data.Models;
using SampleApi.Data.Contracts;
using Microsoft.Extensions.Logging;
using System;
using AutoWrapper.Extensions;
using Microsoft.AspNetCore.Http;

namespace SampleApi.API.v1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IManagerService _managerService;

        public AuthController(IManagerService managerService, ILogger<AuthController> logger)
        {
            _managerService = managerService;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<AuthenticateResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> Authenticate(AuthenticateRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ApiException(ModelState.AllErrors());
                else
                {
                    var response = await _managerService.Authenticate(model, IpAddress());

                    if (response == null)
                        throw new ApiException("Username or password is incorrect.", StatusCodes.Status400BadRequest);

                    SetTokenCookie(response.RefreshToken);

                    return new ApiResponse(response);
                }
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new ApiException(ex);
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<AuthenticateResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];
                var response = await _managerService.RefreshToken(refreshToken, IpAddress());

                if (response == null)
                    throw new ApiException("Invalid Token", StatusCodes.Status401Unauthorized);

                SetTokenCookie(response.RefreshToken);

                return new ApiResponse(response);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new ApiException(ex);
            }
        }

        [HttpPost]
        [Route("RevokeToken")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<AuthenticateResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> RevokeToken()
        {
            try
            {
                // accept token from request body or cookie
                var token = Request.Cookies["refreshToken"];

                var response = await _managerService.RevokeToken(token, IpAddress());
                 
                if (!response)
                    throw new ApiException("Token not found");

                return new ApiResponse("Revoked token");
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new ApiException(ex);
            }
        }


        #region Metodos Privados
        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        #endregion


    }
}
