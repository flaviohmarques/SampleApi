using Newtonsoft.Json;
using SampleApi.Data.Models;
using System.Collections.Generic;

namespace SampleApi.Data.Entities
{
    [BsonCollection("user")]
    public class User : Document
    {
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string TaxPayerNumber { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public List<string> Agreements { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}