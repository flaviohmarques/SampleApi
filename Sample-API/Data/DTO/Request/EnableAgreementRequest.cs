using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Data.DTO.Request
{
    public class EnableAgreementRequest
    {
        public string UserId { get; set; }
        public string AgreementName { get; set; }
    }

    public class EnableAgreementRequestValidator : AbstractValidator<EnableAgreementRequest>
    {
        public EnableAgreementRequestValidator()
        {
            RuleFor(o => o.UserId).NotEmpty().WithMessage("Please inform the UserId.");
            RuleFor(o => o.AgreementName).NotEmpty().WithMessage("Please inform the AgreementName.");
        }
    }
}
