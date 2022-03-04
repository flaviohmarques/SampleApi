using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace SampleApi.Data.DTO.Request
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(o => o.UserName).NotEmpty();
            RuleFor(o => o.Password).NotEmpty();
        }
    }
}
