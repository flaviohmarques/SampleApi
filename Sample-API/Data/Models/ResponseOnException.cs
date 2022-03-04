namespace SampleApi.Data.Models
{
    public class ResponseOnException
    {
        public bool IsError { get; set; }
        public ResponseException ResponseException { get; set; }
    }

    public class ResponseException
    {
        public string ExceptionMessage { get; set; }
    }
}
