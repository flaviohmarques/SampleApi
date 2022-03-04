namespace SampleApi.Data.Models
{
    public class ResponseWrapper<T>
    {
        public int StatusCode { get; set; }
        public T Result { get; set; }
    }
    
}
