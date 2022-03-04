using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Data.DTO.Request
{
    public class RoleRequest
    {
        public string Name { get; set; }
        
        public List<string> Routes { get; set; }

    }

    public class PerfilRequestValidator : AbstractValidator<RoleRequest>
    {
        public PerfilRequestValidator()
        {
            RuleFor(o => o.Name).NotEmpty().WithMessage("Please inform the Name.");
            RuleFor(o => o.Routes).NotEmpty().WithMessage("Please inform the Routes.");
        }
    }
}
