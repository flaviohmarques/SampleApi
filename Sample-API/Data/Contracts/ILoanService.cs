using SampleApi.Data.DTO.Request;
using SampleApi.Data.DTO.Response;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Data.Contracts
{
    public interface ILoanService
    {
        Task<InputResponse> QueryMiscellaneous(QueryMiscellaneousRequest consultaDiversosRequest);
        Task<string> QueryMiscellaneousAsync(QueryMiscellaneousRequest consultaDiversosRequest);
        Task<Dictionary<string, object>> GetFields(GetFieldsRequest obterCamposRequest);
        Task<InputResponse> MarginEndorsement(LoanRequest consignadoRequest);
        Task<string> MarginEndorsementAsync(LoanRequest consignadoRequest);
        Task<InputResponse> MarginExclusion(LoanRequest consignadoRequest);
        Task<string> MarginExclusionAsync(LoanRequest consignadoRequest);
        Task<InputResponse> MarginInquiry(LoanRequest consignadoRequest);
        Task<string> MarginInquiryAsync(LoanRequest consignadoRequest);
        Task<InputResponse> FunctionalStatus(LoanRequest consignadoRequest);
        Task<string> FunctionalStatusAsync(LoanRequest consignadoRequest);
        Task<Dictionary<string, object>> GetAnswer(string idSolicitacao);
    }
}
