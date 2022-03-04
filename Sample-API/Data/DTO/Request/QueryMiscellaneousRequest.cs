using FluentValidation;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace SampleApi.Data.DTO.Request
{
    public class QueryMiscellaneousRequest : DynamicObject
    {
        public QueryMiscellaneousRequest()
        {
            Dictionary = new Dictionary<string, object>();
        }

        public string Method { get; set; }
        public string Operation { get; set; }
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


    public class QueryMiscellaneousRequestValidator : AbstractValidator<QueryMiscellaneousRequest>
    {
        public QueryMiscellaneousRequestValidator()
        {
            RuleFor(o => o.Operation).NotEmpty();
        }
    }
}
