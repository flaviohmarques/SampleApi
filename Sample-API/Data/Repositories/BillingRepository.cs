using SampleApi.Data.Contracts;
using SampleApi.Data.Entities;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;

namespace SampleApi.Data.Repositories
{
    public class BillingRepository : MongoDbFactoryBase, IBillingRepository
    {

        public BillingRepository(IConfiguration configuration) : base(configuration)
        {
       
        }

        public async Task AddBilling(ControlBilling controlBilling)
        {
            controlBilling.DateTime = DateTime.UtcNow.ToLongDateString();
            await InsertOneAsync(controlBilling);
        }
    }
}
