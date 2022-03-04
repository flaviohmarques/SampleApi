
using SampleApi.Data.Models;

namespace SampleApi.Data.Entities
{
    [BsonCollection("parameters_agreement")]
    public class ParametersAgreement : Document
    {
        public string Agreement { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
    }

    public class ParametersAgreementDto
    {
        public string Parameter { get; set; }
        public string Value { get; set; }
    }
}
