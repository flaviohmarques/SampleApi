using SampleApi.Data.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Data.Entities
{
    [BsonCollection("connected_services")]
    public class ConnectedServices : Document
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public Method VerbHttp { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public bool UseProxy { get; set; }
        public string UserProxy { get; set; }
        public string PasswordProxy { get; set; }
    }
}
