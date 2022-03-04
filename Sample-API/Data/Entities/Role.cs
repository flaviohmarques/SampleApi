using SampleApi.Data.Models;
using System.Collections.Generic;

namespace SampleApi.Data.Entities
{
    [BsonCollection("role")]
    public class Role : Document
    {
        public string Name { get; set; }
       
        public List<string> Routes { get; set; }
    }
}