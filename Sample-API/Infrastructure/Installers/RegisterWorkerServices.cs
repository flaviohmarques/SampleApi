using SampleApi.Data.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BMG.Metabusca.Dataprev.AWS.Infrastructure.Installers
{
    internal class RegisterWorkerServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            //Register Hosted Services
        }
    }
}
