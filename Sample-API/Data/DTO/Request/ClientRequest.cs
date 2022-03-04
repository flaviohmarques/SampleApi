using SampleApi.Data.Entities;
using FluentValidation;
using System.Collections.Generic;

namespace SampleApi.Data.DTO.Request
{
    public class ClientRequest
    {
        public string CompanyName { get; set; }
        public string TaxPayerNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string BillingPlan { get; set; }
        public string PaymentType { get; set; }
        public List<ParametersAgreementDto> Parameters { get; set; }

    }

    public class ClientRequestValidator : AbstractValidator<ClientRequest>
    {
        public ClientRequestValidator()
        {
            RuleFor(o => o.CompanyName).NotEmpty().WithMessage("Please inform the CompanyName.");
            RuleFor(o => o.TaxPayerNumber).IsValidCNPJ().WithMessage("Please inform the TaxPayerNumber.");
            RuleFor(o => o.Address).NotEmpty().WithMessage("Please inform the Address.");
            RuleFor(o => o.Email).EmailAddress().WithMessage("Please inform the Email.");
            RuleFor(o => o.BillingPlan).NotEmpty().WithMessage("Please inform the BillingPlan.");
            RuleFor(o => o.PaymentType).NotEmpty().WithMessage("Please inform the PaymentType.");
        }
    }
}
