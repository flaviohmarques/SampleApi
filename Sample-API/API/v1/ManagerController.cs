using SampleApi.Data.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using AutoWrapper.Wrappers;
using System.Threading.Tasks;
using SampleApi.Data.Models;
using SampleApi.Data.Contracts;
using Microsoft.Extensions.Logging;
using System;
using AutoWrapper.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using SampleApi.Data.Entities;

namespace SampleApi.API.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService, ILogger<ManagerController> logger)
        {
            _managerService = managerService;
            _logger = logger;
        }


        [Authorize(Roles = AccessRole.MasterAndAdmin)]
        [HttpPost]
        [Route("RegisterAgreement")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> RegisterAgreement(AgreementRequest registerAgreementRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    return new ApiResponse(await _managerService.RegisterAgreement(registerAgreementRequest));
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

        [Authorize(Roles = AccessRole.MasterAndAdmin)]
        [HttpPost]
        [Route("ChangeAgreement")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> ChangeAgreement(AgreementRequest changeAgreementRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    return new ApiResponse(await _managerService.UpdateAgreement(changeAgreementRequest));
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

        [Authorize(Roles = AccessRole.MasterAndAdmin)]
        [HttpPost]
        [Route("AddClient")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<Client>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> AddClient(ClientRequest clientRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    clientRequest.TaxPayerNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(clientRequest.TaxPayerNumber, @"[^\d]"));
                    return new ApiResponse(await _managerService.AddClient(clientRequest));
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

        [Authorize(Roles = AccessRole.MasterAndAdmin)]
        [HttpPost]
        [Route("AddUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> AddUser(UserRequest userRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    userRequest.TaxPayerNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(userRequest.TaxPayerNumber, @"[^\d]"));
                    return new ApiResponse(await _managerService.AddUser(userRequest));
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

        [Authorize(Roles = AccessRole.MasterAndAdmin)]
        [HttpPost]
        [Route("AddRole")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> AddRole(RoleRequest roleRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    return new ApiResponse(await _managerService.AddRole(roleRequest));
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

        [Authorize(Roles = AccessRole.MasterAndAdmin)]
        [HttpPost]
        [Route("EnableAgreement")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> EnableAgreement(EnableAgreementRequest enableAgreementRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    return new ApiResponse(await _managerService.EnableAgreement(enableAgreementRequest));
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

        [Authorize(Roles = AccessRole.MasterAndAdmin)]
        [HttpPost]
        [Route("AdicionarServicos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> AdicionarServicos(ConnectedServicesRequest adicionarServicosRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    return new ApiResponse(await _managerService.AddConnectedServices(adicionarServicosRequest));
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

        [Authorize(Roles = AccessRole.MasterAndAdmin)]
        [HttpPost]
        [Route("AdicionarUsuarioRobo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> AdicionarServicos(ParametersUserRobotRequest parametroUsuarioRoboRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    return new ApiResponse(await _managerService.AddParametersUserRobot(parametroUsuarioRoboRequest));
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

    }
}
