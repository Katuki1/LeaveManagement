using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Logging;
using HRLeaveManagement.Application.Models.Email;
using HRLeaveManagement.Infastructure.EmailService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Infastructure
{
    public static class RegisterServiceInfrastructure 
    {
        public static IServiceCollection ConfigureInfrastructureServices (this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }

    }
}
