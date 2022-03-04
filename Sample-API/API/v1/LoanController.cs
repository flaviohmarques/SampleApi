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
using SampleApi.Data.DTO.Response;
using Newtonsoft.Json;
using System.Collections.Generic;
using SampleApi.Data.Entities;

namespace SampleApi.API.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class LoanController : ControllerBase
    {
        private readonly ILogger<LoanController> _logger;
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService, ILogger<LoanController> logger)
        {
            _loanService = loanService;
            _logger = logger;
        }

        [Authorize(Roles = AccessRole.MasterAndCliente)]
        [HttpPost]
        [Route("MarginInquiry")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<InputResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> MarginInquiry(LoanRequest loanRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    return new ApiResponse(await _loanService.MarginInquiry(loanRequest));
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

        [Authorize(Roles = AccessRole.MasterAndCliente)]
        [HttpPost]
        [Route("FunctionalStatus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<InputResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> FunctionalStatus(LoanRequest loanRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    return new ApiResponse(await _loanService.FunctionalStatus(loanRequest));
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

        [Authorize(Roles = AccessRole.MasterAndCliente)]
        [HttpPost]
        [Route("MarginEndorsement")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<InputResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> MarginEndorsement(LoanRequest loanRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
            {
                try
                {
                    return new ApiResponse(await _loanService.MarginEndorsement(loanRequest));
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

        [Authorize(Roles = AccessRole.MasterAndCliente)]
        [HttpPost]
        [Route("MarginExclusion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<InputResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> MarginExclusion(LoanRequest loanRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
            {
                try
                {
                    return new ApiResponse(await _loanService.MarginExclusion(loanRequest));
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

        [Authorize(Roles = AccessRole.MasterAndCliente)]
        [HttpPost]
        [Route("QueryMiscellaneous")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<InputResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> QueryMiscellaneous(QueryMiscellaneousRequest queryMiscellaneousRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    return new ApiResponse(await _loanService.QueryMiscellaneous(queryMiscellaneousRequest));
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

        [Authorize(Roles = AccessRole.MasterAndCliente)]
        [HttpPost]
        [Route("GetFields")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<Dictionary<string, object>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> GetFields(GetFieldsRequest getFieldsRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
            {
                try
                {
                    return new ApiResponse(await _loanService.GetFields(getFieldsRequest));
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

        [Authorize(Roles = AccessRole.MasterAndCliente)]
        [HttpGet]
        [Route("GetAnswer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<Dictionary<string, object>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> GetAnswer(string idRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
            {
                try
                {
                    return new ApiResponse(await _loanService.GetAnswer(idRequest));
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

        [Authorize(Roles = AccessRole.MasterAndCliente)]
        [HttpPost]
        [Route("MarginInquiryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> MarginInquiryAsync(LoanRequest loanRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    return new ApiResponse(await _loanService.MarginInquiryAsync(loanRequest));
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

        [Authorize(Roles = AccessRole.MasterAndCliente)]
        [HttpPost]
        [Route("FunctionalStatusAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> FunctionalStatusAsync(LoanRequest loanRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    return new ApiResponse(await _loanService.FunctionalStatusAsync(loanRequest));
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

        [Authorize(Roles = AccessRole.MasterAndCliente)]
        [HttpPost]
        [Route("MarginEndorsementAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> MarginEndorsementAsync(LoanRequest loanRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
            {
                try
                {
                    return new ApiResponse(await _loanService.MarginEndorsementAsync(loanRequest));
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

        [Authorize(Roles = AccessRole.MasterAndCliente)]
        [HttpPost]
        [Route("MarginExclusionAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> MarginExclusionAsync(LoanRequest loanRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
            {
                try
                {
                    return new ApiResponse(await _loanService.MarginExclusionAsync(loanRequest));
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

        [Authorize(Roles = AccessRole.MasterAndCliente)]
        [HttpPost]
        [Route("QueryMiscellaneousAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseWrapper<ResponseModelException>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseWrapper<ResponseOnException>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseWrapper<ResponseOnException>))]
        public async Task<ApiResponse> QueryMiscellaneousAsync(QueryMiscellaneousRequest queryMiscellaneousRequest)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            else
                try
                {
                    return new ApiResponse(await _loanService.QueryMiscellaneousAsync(queryMiscellaneousRequest));
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
