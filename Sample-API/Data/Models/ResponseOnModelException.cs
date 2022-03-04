using System.Collections.Generic;

namespace SampleApi.Data.Models
{
    public class ResponseOnModelException
    {
        public bool IsError { get; set; }
        public ResponseModelException ResponseException { get; set; }
    }

    public class ResponseModelException
    {
        public string ExceptionMessage { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
    }

    public class ValidationError
    {
        public string Name { get; set; }
        public string Reason { get; set; }
    }

}
