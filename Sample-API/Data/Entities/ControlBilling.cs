using SampleApi.Data.Models;

namespace SampleApi.Data.Entities
{
    [BsonCollection("control_billing")]
    public class ControlBilling : Document
    {
        public string UserName { get; set; }
        public string IdUser { get; set; }
        public string DateTime { get; set; }
        public string TypeUsed { get; set; }
        public string ExternalIdentification { get; set; }
        public string InputId { get; set; }

        public ControlBilling(string userName, string idUser, string externalIdentification, string inputId, string typeUsed)
        {
            UserName = userName;
            IdUser = idUser;
            TypeUsed = typeUsed;
            ExternalIdentification = externalIdentification;
            InputId = inputId;
        }
    }
}
