using FluentValidation;

namespace SampleApi.Data.DTO.Request
{
    public class GetFieldsRequest
    {
        public string Agreement { get; set; }
        public string Operation { get; set; }
    }

    public class GetFieldsRequestValidator : AbstractValidator<GetFieldsRequest>
    {
        public GetFieldsRequestValidator()
        {
            RuleFor(o => o.Agreement).NotEmpty();
            RuleFor(o => o.Operation).NotEmpty();
        }
    }
}
