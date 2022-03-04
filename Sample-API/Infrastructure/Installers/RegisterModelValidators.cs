using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApi.Data.Contracts;
using SampleApi.Data.DTO.Request;

namespace SampleApi.Infrastructure.Installers
{
    internal class RegisterModelValidators : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            //Register DTO Validators
            services.AddTransient<IValidator<GetFieldsRequest>, GetFieldsRequestValidator>();
            services.AddTransient<IValidator<AuthenticateRequest>, AuthenticateRequestValidator>();
            services.AddTransient<IValidator<UserRequest>, UsuarioRequestValidator>();
            services.AddTransient<IValidator<LoanRequest>, LoanRequestValidator>();
            services.AddTransient<IValidator<AgreementRequest>, AgreementRequestValidator>();
            services.AddTransient<IValidator<EnableAgreementRequest>, EnableAgreementRequestValidator>();
            services.AddTransient<IValidator<RoleRequest>, PerfilRequestValidator>();
            services.AddTransient<IValidator<ClientRequest>, ClientRequestValidator>();
            services.AddTransient<IValidator<QueryMiscellaneousRequest>, QueryMiscellaneousRequestValidator>();
            services.AddTransient<IValidator<ConnectedServicesRequest>, ConnectedServicesRequestValidator>();
            services.AddTransient<IValidator<ParametersUserRobotRequest>, ParametersUserRobotRequestValidator>();
            //Disable Automatic Model State Validation built-in to ASP.NET Core
            services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = true; });
        }
    }
}