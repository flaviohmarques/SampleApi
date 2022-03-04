using SampleApi.Data.Entities;
using FluentValidation;
using System.Collections.Generic;

namespace SampleApi.Data.DTO.Request
{
    public class ParametersUserRobotRequest
    {
        public string Agreement { get; set; }
        public string Group { get; set; }
        public List<ParametersAgreementDto> Data { get; set; }
    }

    public class ParametersUserRobotRequestValidator : AbstractValidator<ParametersUserRobotRequest>
    {
        public ParametersUserRobotRequestValidator()
        {
            RuleFor(o => o.Agreement).NotEmpty();
            RuleFor(o => o.Group).NotEmpty();
            RuleFor(o => o.Data).NotEmpty();
        }
    }
}
