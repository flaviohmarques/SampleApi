using SampleApi.Data.Contracts;
using SampleApi.Data.DTO.Response;
using SampleApi.Data.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace SampleApi.Data.Repositories
{
    public class InputRepository : MongoDbFactoryBase, IInputRepository
    {

        public InputRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task UpdateAnswerInput(string idInput, InputResponse inputResponse, DateTime dateAnswer)
        {
            Input solicitacao = await FindByIdAsync<Input>(idInput);
            solicitacao.DateAnswer = dateAnswer;
            solicitacao.AnswerData = inputResponse.ResponseDictionary;
            await ReplaceOneAsync<Input>(solicitacao, solicitacao.Id);
        }

        public async Task<Input> CreateInput(Input input)
        {
            return await InsertOneAsync(input);
        }

        public async Task<OperationsAgreement> GetOperation(string agreement, string operation)
        {
            return await FindOneAsync<OperationsAgreement>(x => x.Agreement == agreement && x.Operation == operation);
        }

        public async Task<Input> GetInput(string idInput)
        {
            return await FindByIdAsync<Input>(idInput);
        }

    }
}
