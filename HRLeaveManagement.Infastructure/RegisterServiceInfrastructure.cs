using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Infastructure
{
    public static class RegisterServiceInfrastructure 
    {
        public static IServiceCollection ConfigureInfrastructureServices (this IServiceCollection services,
            IConfiguration configuration)
        {


            return services;
        }

    }
}
