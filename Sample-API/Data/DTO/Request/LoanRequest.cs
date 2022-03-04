using FluentValidation;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace SampleApi.Data.DTO.Request
{
    public class LoanRequest : DynamicObject
    {
        public LoanRequest()
        {
            Dictionary = new Dictionary<string, object>();
        }
        public string Agreement { get; set; }
        public string TaxPayerNumber { get; set; }
        public string EnrollNumber { get; set; }
        public string OriginIdentifier { get; set; }

        public Dictionary<string, object> Dictionary { get; set; }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name;
            return Dictionary.TryGetValue(name, out result);
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Dictionary[binder.Name] = value;
            return true;
        }
    }


    public class LoanRequestValidator : AbstractValidator<LoanRequest>
    {
        public LoanRequestValidator()
        {
            RuleFor(o => o.Agreement).NotEmpty();
            RuleFor(o => o.TaxPayerNumber).NotEmpty();
            RuleFor(o => o.EnrollNumber).NotEmpty();
            RuleFor(o => o.OriginIdentifier).NotEmpty();
        }
    }
}
