using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CommissionX.Infrastructure.Data
{
    public class CommissionDataContextFactory : IDesignTimeDbContextFactory<CommissionDataContext>
    {
        public CommissionDataContext CreateDbContext(string[] args)
        {
            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // get connectionstring
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configure shared CafeManagementDbContext
            var optionsBuilder = new DbContextOptionsBuilder<CommissionDataContext>();
            optionsBuilder
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    builder => builder.MigrationsAssembly(typeof(CommissionDataContext).Assembly.FullName));

            // Return the DbContext instance using the default constructor
            return new CommissionDataContext(optionsBuilder.Options);

        }
    }
}