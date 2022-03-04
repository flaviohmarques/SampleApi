using SampleApi.Data.Models;
using System.Collections.Generic;

namespace SampleApi.Data.Entities
{
    public class Client : Document
    {
        public string CompanyName { get; set; }
        public string TaxPayerNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string BillingPlan { get; set; }
        public string PaymentType { get; set; }
        public List<ParametersAgreementDto> Parameters { get; set; }
    }
}
