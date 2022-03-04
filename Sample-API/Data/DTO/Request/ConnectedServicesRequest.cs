using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Data.DTO.Request
{
    public class ConnectedServicesRequest
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string VerbHttp { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public bool UseProxy { get; set; }
        public string UserProxy { get; set; }
        public string PasswordProxy { get; set; }
    }

    public class ConnectedServicesRequestValidator : AbstractValidator<ConnectedServicesRequest>
    {
        public ConnectedServicesRequestValidator()
        {
            RuleFor(o => o.Name).NotEmpty();
            RuleFor(o => o.Url).NotEmpty();
            RuleFor(o => o.VerbHttp).NotEmpty();
            RuleFor(o => o.Type).NotEmpty();
            RuleFor(o => o.Status).NotNull();
            RuleFor(o => o.UseProxy).NotNull();
        }
    }
}
