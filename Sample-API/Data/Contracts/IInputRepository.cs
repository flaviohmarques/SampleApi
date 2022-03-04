using SampleApi.Data.DTO.Response;
using SampleApi.Data.Entities;
using System;
using System.Threading.Tasks;

namespace SampleApi.Data.Contracts
{
    public interface IInputRepository
    {
        Task<Input> CreateInput(Input solicitacao);

        Task<Input> GetInput(string idSolicitacao);

        Task UpdateAnswerInput(string idSolicitacao, InputResponse solicitacaoResponse, DateTime dataResposta);

        Task<OperationsAgreement> GetOperation(string convenio, string operacao);
    }
}
