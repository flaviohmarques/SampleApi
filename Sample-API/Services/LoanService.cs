using AsyncAwaitBestPractices;
using AutoWrapper.Wrappers;
using SampleApi.Data.Contracts;
using SampleApi.Data.DTO.Request;
using SampleApi.Data.DTO.Response;
using SampleApi.Data.Entities;
using SampleApi.Data.Models;
using SampleApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleApi.Services
{
    public class LoanService : ILoanService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications

        private readonly AppSettings _appSettings;
        private readonly IInputRepository _inputRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IBillingRepository _billingRepository;
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<LoanService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public LoanService(IOptions<AppSettings> appSettings, IInputRepository solicitacaoRepository, IUsersRepository usersRepository, IBillingRepository billingRepository, IConfiguration configuration, IDistributedCache distributedCache, ILogger<LoanService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = appSettings.Value;
            _inputRepository = solicitacaoRepository;
            _usersRepository = usersRepository;
            _billingRepository = billingRepository;
            _configuration = configuration;
            _distributedCache = distributedCache;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<InputResponse> QueryMiscellaneous(QueryMiscellaneousRequest consultaDiversosRequest)
        {
            string idInput = string.Empty;
            string user = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            try
            {
                OperationsAgreement operationsAgreement = await GetAgreementOperation(consultaDiversosRequest.Method, consultaDiversosRequest.Operation);
                Input solicitacao = await _inputRepository.CreateInput(new Input(user, DateTime.UtcNow, consultaDiversosRequest.Method, operationsAgreement.Module, operationsAgreement.Operation, consultaDiversosRequest.Dictionary));
                idInput = solicitacao.Id.ToString();
                return await RequestInput(solicitacao);
            }
            catch (Exception ex)
            {
                string message = $"ConsultaDiversos - {ex.Message}";
                _logger.LogInformation(message);
                throw new ApiException(ex, 400);
            }
            finally
            {
                await _billingRepository.AddBilling(new ControlBilling(user, _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value, consultaDiversosRequest.OriginIdentifier, idInput, "ConsultaDiversos"));
            }
        }

        public async Task<Dictionary<string, object>> GetFields(GetFieldsRequest getFieldsRequest)
        {
            OperationsAgreement operationsAgreement = await GetAgreementOperation(getFieldsRequest.Agreement.ToLower(), getFieldsRequest.Operation.ToLower());
            return operationsAgreement.Fields;
        }

        public async Task<InputResponse> MarginEndorsement(LoanRequest loanRequest)
        {
            string idInput = string.Empty;
            string user = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            try
            {
                loanRequest.Dictionary.TryAdd("convenio", loanRequest.Agreement);
                loanRequest.Dictionary.TryAdd("cpf", loanRequest.TaxPayerNumber);
                loanRequest.Dictionary.TryAdd("matricula", loanRequest.EnrollNumber);
                OperationsAgreement operationsAgreement = await GetAgreementOperation(loanRequest.Agreement, "averbacao");
                Input input = await _inputRepository.CreateInput(new Input(user, DateTime.UtcNow, loanRequest.Agreement, operationsAgreement.Module, operationsAgreement.Operation, loanRequest.Dictionary));
                idInput = input.Id.ToString();
                return await RequestInput(input);
            }
            catch (Exception ex)
            {
                string message = $"AgregacaoMargem - {ex.Message}";
                _logger.LogInformation(message);
                throw new ApiException(ex, 400);
            }
            finally
            {
                await _billingRepository.AddBilling(new ControlBilling(user, _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value, loanRequest.OriginIdentifier, idInput, "AgregacaoMargem"));
            }

        }

        public async Task<InputResponse> MarginInquiry(LoanRequest loanRequest)
        {
            string idSolicitacao = string.Empty;
            string usuario = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            try
            {
                loanRequest.Dictionary.TryAdd("convenio", loanRequest.Agreement);
                loanRequest.Dictionary.TryAdd("cpf", loanRequest.TaxPayerNumber);
                loanRequest.Dictionary.TryAdd("matricula", loanRequest.EnrollNumber);
                OperationsAgreement conveniosOperacao = await GetAgreementOperation(loanRequest.Agreement, "consultamargem");
                Input solicitacao = await _inputRepository.CreateInput(new Input(usuario, DateTime.UtcNow, loanRequest.Agreement, conveniosOperacao.Module, conveniosOperacao.Operation, loanRequest.Dictionary));
                idSolicitacao = solicitacao.Id.ToString();
                return await RequestInput(solicitacao);
            }
            catch (Exception ex)
            {
                string message = $"MarginInquiry - {ex.Message}";
                _logger.LogInformation(message);
                throw new ApiException(ex, 400);
            }
            finally
            {
                await _billingRepository.AddBilling(new ControlBilling(usuario, _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value, loanRequest.OriginIdentifier, idSolicitacao, "ConsultaMargem"));
            }
        }

        public async Task<InputResponse> FunctionalStatus(LoanRequest loanRequest)
        {
            string idSolicitacao = string.Empty;
            string usuario = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            try
            {
                loanRequest.Dictionary.TryAdd("convenio", loanRequest.Agreement);
                loanRequest.Dictionary.TryAdd("cpf", loanRequest.TaxPayerNumber);
                loanRequest.Dictionary.TryAdd("matricula", loanRequest.EnrollNumber);
                OperationsAgreement conveniosOperacao = await GetAgreementOperation(loanRequest.Agreement, "situacaofuncional");
                Input solicitacao = await _inputRepository.CreateInput(new Input(usuario, DateTime.UtcNow, loanRequest.Agreement, conveniosOperacao.Module, conveniosOperacao.Operation, loanRequest.Dictionary));
                idSolicitacao = solicitacao.Id.ToString();
                return await RequestInput(solicitacao);
            }
            catch (Exception ex)
            {
                string message = $"FunctionalStatus - {ex.Message}";
                _logger.LogInformation(message);
                throw new ApiException(ex, 400);
            }
            finally
            {
                await _billingRepository.AddBilling(new ControlBilling(usuario, _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value, loanRequest.OriginIdentifier, idSolicitacao, "SituacaoFuncional"));
            }
        }

        public async Task<InputResponse> MarginExclusion(LoanRequest loanRequest)
        {
            string idSolicitacao = string.Empty;
            string usuario = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            try
            {
                OperationsAgreement conveniosOperacao = await GetAgreementOperation(loanRequest.Agreement, "marginexclusion");
                Input solicitacao = await _inputRepository.CreateInput(new Input(usuario, DateTime.UtcNow, loanRequest.Agreement, conveniosOperacao.Module, conveniosOperacao.Operation, loanRequest.Dictionary));
                idSolicitacao = solicitacao.Id.ToString();
                return await RequestInput(solicitacao);
            }
            catch (Exception ex)
            {
                string message = $"MarginExclusion - {ex.Message}";
                _logger.LogInformation(message);
                throw new ApiException(ex, 400);
            }
            finally
            {
                await _billingRepository.AddBilling(new ControlBilling(usuario, _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value, loanRequest.OriginIdentifier, idSolicitacao, "MarginExclusion"));
            }
        }

        private async Task<OperationsAgreement> GetAgreementOperation(string agreement, string operation)
        {
            OperationsAgreement operationsAgreement = await _inputRepository.GetOperation(agreement, operation);
            if (operationsAgreement == null)
                throw new ApiException("Operations agreement suplied was not located for this operation!");
            return operationsAgreement;
        }

        private async Task<InputResponse> RequestInput(Input input)
        {
            InputExecutor inputExecutor = new()
            {
                NameModule = input.Module,
                ParamsModule = new List<object>(),
                ParamsStart = new List<object>()
            };
            input.EntryData.Add("id", input.Id.ToString());
            inputExecutor.ParamsStart.Add(input.EntryData);
            ConnectedServices servico = await GetService();
            return await AnswerInput(servico, input, inputExecutor);

        }

        private async Task<ConnectedServices> GetService()
        {
            var listServices = await _usersRepository.GetConnectedServices("default");
            var service = listServices.FirstOrDefault();
            return service;
        }

        private async Task<string> RequestInputAsync(Input solicitacao)
        {
            InputExecutor inputExecutor = new()
            {
                NameModule = solicitacao.Module,
                ParamsModule = new List<object>(),
                ParamsStart = new List<object>()
            };
            solicitacao.EntryData.Add("id", solicitacao.Id.ToString());
            inputExecutor.ParamsStart.Add(solicitacao.EntryData);
            ConnectedServices servico = await GetService();
            AnswerInput(servico, solicitacao, inputExecutor).SafeFireAndForget(onException: (exception) =>
            {
                _logger.LogError(exception, $"Fail to answer input.");
            });
            return solicitacao.Id.ToString();
        }

        private async Task<InputResponse> AnswerInput(ConnectedServices servicosConectados, Input input, InputExecutor inputExecutor)
        {
            var client = new RestClient(servicosConectados.Url)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            string json = JsonConvert.SerializeObject(inputExecutor, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }, NullValueHandling = NullValueHandling.Ignore });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse<ExecutorResponse> response = await client.ExecuteAsync<ExecutorResponse>(request);
            if (response.IsSuccessful)
            {
                if (response.Data.Success)
                {
                    InputResponse inputResponse = new()
                    {
                        OperationId = input.Id.ToString(),
                        Code = response.Data.Msg.Code,
                        Description = response.Data.Msg.Description,
                        ResponseDictionary = response.Data.Msg.Result
                    };

                    _inputRepository.UpdateAnswerInput(input.Id.ToString(), inputResponse, DateTime.UtcNow).SafeFireAndForget(onException: (exception) =>
                    {
                        _logger.LogError(exception, $"Fail to update the answer input.");
                    });

                    return inputResponse;
                }
                else
                {
                    throw new ApiException(response.Data.Msg, StatusCodes.Status412PreconditionFailed);
                }
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.InternalServerError || response.StatusCode == HttpStatusCode.FailedDependency)
            {
                throw new ApiException("Favor verifique sua requisição", StatusCodes.Status412PreconditionFailed);
            }
            else if (response.Data != null && response.Data.Msg != null)
            {
                InputResponse solicitacaoResponse = new()
                {
                    OperationId = input.Id.ToString(),
                    Code = response.Data.Msg.Code,
                    Description = response.Data.Msg.Description,
                    ResponseDictionary = response.Data.Msg.Result
                };

                _inputRepository.UpdateAnswerInput(input.Id.ToString(), solicitacaoResponse, DateTime.UtcNow).SafeFireAndForget(onException: (exception) =>
                {
                    _logger.LogError(exception, $"Falha ao atualizar resposta da solicitacao");
                });

                throw new ApiException(JsonConvert.SerializeObject(response.Data.Msg), StatusCodes.Status422UnprocessableEntity);
            }
            throw new ApiException("Exceção não tratada", StatusCodes.Status500InternalServerError);
        }

        public async Task<string> QueryMiscellaneousAsync(QueryMiscellaneousRequest consultaDiversosRequest)
        {
            string idSolicitacao = string.Empty;
            string usuario = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            try
            {
                OperationsAgreement conveniosOperacao = await GetAgreementOperation(consultaDiversosRequest.Method, consultaDiversosRequest.Operation);
                Input solicitacao = await _inputRepository.CreateInput(new Input(usuario, DateTime.UtcNow, consultaDiversosRequest.Method, conveniosOperacao.Module, conveniosOperacao.Operation, consultaDiversosRequest.Dictionary));
                idSolicitacao = solicitacao.Id.ToString();
                return await RequestInputAsync(solicitacao);
            }
            catch (Exception ex)
            {
                string message = $"QueryMiscellaneousAsync - {ex.Message}";
                _logger.LogInformation(message);
                throw new ApiException(ex, 400);
            }
            finally
            {
                await _billingRepository.AddBilling(new ControlBilling(usuario, _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value, consultaDiversosRequest.OriginIdentifier, idSolicitacao, "ConsultaDiversos"));
            }
        }

        public async Task<string> MarginEndorsementAsync(LoanRequest loanRequest)
        {
            string idSolicitacao = string.Empty;
            string usuario = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            try
            {
                loanRequest.Dictionary.TryAdd("convenio", loanRequest.Agreement);
                loanRequest.Dictionary.TryAdd("cpf", loanRequest.TaxPayerNumber);
                loanRequest.Dictionary.TryAdd("matricula", loanRequest.EnrollNumber);
                OperationsAgreement conveniosOperacao = await GetAgreementOperation(loanRequest.Agreement, "averbacao");
                Input solicitacao = await _inputRepository.CreateInput(new Input(usuario, DateTime.UtcNow, loanRequest.Agreement, conveniosOperacao.Module, conveniosOperacao.Operation, loanRequest.Dictionary));
                idSolicitacao = solicitacao.Id.ToString();
                return await RequestInputAsync(solicitacao);
            }
            catch (Exception ex)
            {
                string message = $"AgregacaoMargem - {ex.Message}";
                _logger.LogInformation(message);
                throw new ApiException(ex, 400);
            }
            finally
            {
                await _billingRepository.AddBilling(new ControlBilling(usuario, _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value, loanRequest.OriginIdentifier, idSolicitacao, "AgregacaoMargem"));
            }
        }

        public async Task<string> MarginExclusionAsync(LoanRequest consignadoRequest)
        {
            string idSolicitacao = string.Empty;
            string usuario = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            try
            {
                OperationsAgreement conveniosOperacao = await GetAgreementOperation(consignadoRequest.Agreement, "marginexclusion");
                Input solicitacao = await _inputRepository.CreateInput(new Input(usuario, DateTime.UtcNow, consignadoRequest.Agreement, conveniosOperacao.Module, conveniosOperacao.Operation, consignadoRequest.Dictionary));
                idSolicitacao = solicitacao.Id.ToString();
                return await RequestInputAsync(solicitacao);
            }
            catch (Exception ex)
            {
                string message = $"MarginExclusionAsync - {ex.Message}";
                _logger.LogInformation(message);
                throw new ApiException(ex, 400);
            }
            finally
            {
                await _billingRepository.AddBilling(new ControlBilling(usuario, _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value, consignadoRequest.OriginIdentifier, idSolicitacao, "ExclusaoMargem"));
            }
        }

        public async Task<string> MarginInquiryAsync(LoanRequest consignadoRequest)
        {
            string idSolicitacao = string.Empty;
            string usuario = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            try
            {
                consignadoRequest.Dictionary.TryAdd("convenio", consignadoRequest.Agreement);
                consignadoRequest.Dictionary.TryAdd("cpf", consignadoRequest.TaxPayerNumber);
                consignadoRequest.Dictionary.TryAdd("matricula", consignadoRequest.EnrollNumber);
                OperationsAgreement conveniosOperacao = await GetAgreementOperation(consignadoRequest.Agreement, "consultamargem");
                Input solicitacao = await _inputRepository.CreateInput(new Input(usuario, DateTime.UtcNow, consignadoRequest.Agreement, conveniosOperacao.Module, conveniosOperacao.Operation, consignadoRequest.Dictionary));
                idSolicitacao = solicitacao.Id.ToString();
                return await RequestInputAsync(solicitacao);
            }
            catch (Exception ex)
            {
                string message = $"ConsultaMargem - {ex.Message}";
                _logger.LogInformation(message);
                throw new ApiException(ex, 400);
            }
            finally
            {
                await _billingRepository.AddBilling(new ControlBilling(usuario, _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value, consignadoRequest.OriginIdentifier, idSolicitacao, "ConsultaMargem"));
            }
        }

        public async Task<string> FunctionalStatusAsync(LoanRequest loanRequest)
        {
            string idSolicitacao = string.Empty;
            string usuario = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            try
            {
                loanRequest.Dictionary.TryAdd("convenio", loanRequest.Agreement);
                loanRequest.Dictionary.TryAdd("cpf", loanRequest.TaxPayerNumber);
                loanRequest.Dictionary.TryAdd("matricula", loanRequest.EnrollNumber);
                OperationsAgreement conveniosOperacao = await GetAgreementOperation(loanRequest.Agreement, "situacaofuncional");
                Input solicitacao = await _inputRepository.CreateInput(new Input(usuario, DateTime.UtcNow, loanRequest.Agreement, conveniosOperacao.Module, conveniosOperacao.Operation, loanRequest.Dictionary));
                idSolicitacao = solicitacao.Id.ToString();
                return await RequestInputAsync(solicitacao);
            }
            catch (Exception ex)
            {
                string message = $"SituacaoFuncional - {ex.Message}";
                _logger.LogInformation(message);
                throw new ApiException(ex, 400);
            }
            finally
            {
                await _billingRepository.AddBilling(new ControlBilling(usuario, _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value, loanRequest.OriginIdentifier, idSolicitacao, "SituacaoFuncional"));
            }
        }

        public async Task<Dictionary<string, object>> GetAnswer(string idSolicitacao)
        {
            Input input = await _inputRepository.GetInput(idSolicitacao);
            return input.AnswerData;
        }
    }
}