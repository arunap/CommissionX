using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Rules;
using Microsoft.EntityFrameworkCore;

namespace CommissionX.Core.Interfaces
{
    public interface ICommissionDataContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<SalesPerson> SalesPersons { get; set; }
        public DbSet<CommissionRule> CommissionRules { get; set; }
        public DbSet<InvoiceProduct> InvoiceProducts { get; set; }
        public DbSet<TireCommissionRuleItem> TireCommissionRuleItems { get; set; }
        public DbSet<ProductCommissionRule> ProductCommissionRules { get; set; }
        public DbSet<SalesPersonCommissionRule> SalesPersonCommissionRules { get; set; }
    }
}