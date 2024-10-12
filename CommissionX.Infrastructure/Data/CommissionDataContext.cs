using System.Reflection;
using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Rules;
using CommissionX.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommissionX.Infrastructure.Data
{
    public class CommissionDataContext : DbContext, ICommissionDataContext
    {
        public new DbSet<T> Set<T>() where T : class => base.Set<T>();
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<SalesPerson> SalesPersons { get; set; }
        public DbSet<CommissionRule> CommissionRules { get; set; }
        public DbSet<InvoiceProduct> InvoiceProducts { get; set; }
        public DbSet<TireCommissionRuleItem> TireCommissionRuleItems { get; set; }
        public DbSet<ProductCommissionRule> ProductCommissionRules { get; set; }
        public DbSet<SalesPersonCommissionRule> SalesPersonCommissionRules { get; set; }

        public CommissionDataContext(DbContextOptions<CommissionDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply model builder configurations from Configuration Assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // modelBuilder.SeedFromJson<Product>("Data/MockData/Products.json");
            // modelBuilder.SeedFromJson<FlatCommissionRule>("Data/MockData/CommissionRules.json");
            // modelBuilder.SeedFromJson<FlatCommissionRule>("Data/MockData/FlatRules.json");

            base.OnModelCreating(modelBuilder);

            // modelBuilder.SeedFromJson<TireCommisionRule>("Data/MockData/TiredRules.json");
            // modelBuilder.SeedFromJson<PercentageCommisionRule>("Data/MockData/PercentageRules.json");
            // modelBuilder.SeedFromJson<CapCommisionRule>("Data/MockData/CapRules.json");
        }

    }
}