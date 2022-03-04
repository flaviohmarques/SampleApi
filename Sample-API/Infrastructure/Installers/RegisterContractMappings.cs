using SampleApi.Data.Contracts;
using SampleApi.Data.Repositories;
using SampleApi.Helpers;
using SampleApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SampleApi.Infrastructure.Installers
{
    internal class RegisterContractMappings : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            //Register Interface Mappings for Repositories
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IBillingRepository, BillingRepository>();
            services.AddScoped<IInputRepository, InputRepository>();
            services.AddScoped<IManagerService, ManagerService>();          
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
        }
    }
}
