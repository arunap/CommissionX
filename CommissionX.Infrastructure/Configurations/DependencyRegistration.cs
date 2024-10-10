using CommissionX.Core.Interfaces;
using CommissionX.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommissionX.Infrastructure.Configurations
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CommissionDataContext>(options => options.UseInMemoryDatabase("CommissionX_DB"));

            services.AddScoped<ICommissionDataContext>(provider => provider.GetRequiredService<CommissionDataContext>());

            services.AddScoped<SeedDataInitializer>();

            return services;
        }
    }
}