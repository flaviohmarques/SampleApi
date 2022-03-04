using SampleApi.Data.Models;
using System.Collections.Generic;

namespace SampleApi.Data.Entities
{
    [BsonCollection("parameters_user_robot")]
    public class ParametersUserRobot : Document
    {
        public string Agreement { get; set; }
        public string Group { get; set; }
        public List<ParametersAgreementDto> Data { get; set; }
    }
}
  