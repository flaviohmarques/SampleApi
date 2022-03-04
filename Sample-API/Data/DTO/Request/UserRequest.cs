using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Data.DTO.Request
{
    public class UserRequest
    {
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string TaxPayerNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UsuarioRequestValidator : AbstractValidator<UserRequest>
    {
        public UsuarioRequestValidator()
        {
            RuleFor(o => o.Name).NotEmpty().WithMessage("Please inform the Name.");
            RuleFor(o => o.LastName).NotEmpty().WithMessage("Please inform the LastName.");
            RuleFor(o => o.TaxPayerNumber).NotEmpty().WithMessage("Please inform the TaxPayerNumber.");
            RuleFor(o => o.Email).EmailAddress().WithMessage("Please inform a valid Email.");
            RuleFor(o => o.Role).NotEmpty().WithMessage("Please inform the Role.");
            RuleFor(o => o.UserName).NotEmpty().WithMessage("Please inform the UserName.");        
            RuleFor(o => o.Password).NotEmpty().WithMessage("Please inform the Password.");
        }
    }
}
