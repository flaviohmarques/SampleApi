using SampleApi.Data.Models;
using System.Collections.Generic;

namespace SampleApi.Data.Entities
{
    [BsonCollection("operations_agreement")]
    public class OperationsAgreement : Document
    {
        public string Agreement { get; set; }
        public string Module { get; set; }
        public string Operation { get; set; }
        public Dictionary<string,object> Fields { get; set; }
        public List<ParametersAgreementDto> Parameters { get; set; }
    }
}