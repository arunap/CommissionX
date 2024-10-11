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
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<CommissionDataContext>(options => options.UseInMemoryDatabase("CommissionX_DB"));
            }
            else
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                // Configure shared CafeManagementDbContext
                services.AddDbContext<CommissionDataContext>(
                    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    builder => builder.MigrationsAssembly(typeof(CommissionDataContext).Assembly.FullName)
                ));
            }

            services.AddScoped<ICommissionDataContext>(provider => provider.GetRequiredService<CommissionDataContext>());

            services.AddScoped<SeedDataInitializer>();

            return services;
        }
    }
}