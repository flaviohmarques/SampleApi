using SampleApi.Data.Entities;
using System.Threading.Tasks;

namespace SampleApi.Data.Contracts
{
    public interface IBillingRepository
    {
        Task AddBilling(ControlBilling controleCobranca);
    }
}