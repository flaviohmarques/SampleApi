using SampleApi.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Data.DTO.Request
{
    public class AgreementRequest
    {
        public string Name { get; set; }
        public string Module { get; set; }
        public string Operation { get; set; }
        public Dictionary<string, object> Fields { get; set; }
        public List<ParametersAgreementDto> Parameters { get; set; }
    }

    public class AgreementRequestValidator : AbstractValidator<AgreementRequest>
    {
        public AgreementRequestValidator()
        {
            RuleFor(o => o.Name).NotEmpty().WithMessage("Please inform the Name.");
            RuleFor(o => o.Module).NotEmpty().WithMessage("Please inform the Module.");
            RuleFor(o => o.Operation).NotEmpty().WithMessage("Please inform the Operation.");
            RuleFor(o => o.Fields).NotEmpty().WithMessage("Please inform the Fields.");
        }
    }
}
