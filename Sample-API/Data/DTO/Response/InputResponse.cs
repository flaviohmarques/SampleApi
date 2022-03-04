using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Data.DTO.Response
{
    public class InputResponse 
    {
        public string OperationId { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> ResponseDictionary { get; set; }
    }
}
