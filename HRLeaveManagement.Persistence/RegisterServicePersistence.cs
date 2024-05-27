using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Persistence.Database;
using HRLeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Persistence
{
    public static class RegisterServicePersistence
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<HrDbContext>(options =>{
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

            return services;

        }

    }
}
