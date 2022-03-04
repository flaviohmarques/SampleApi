using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Data.Entities
{
    public class Msg
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("result")]
        public Dictionary<string,object> Result { get; set; }
    }

    public class ExecutorResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("msg")]
        public Msg Msg { get; set; }
    }
}
