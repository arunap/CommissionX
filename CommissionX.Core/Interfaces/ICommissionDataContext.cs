using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Comissions;
using Microsoft.EntityFrameworkCore;

namespace CommissionX.Core.Interfaces
{
    public interface ICommissionDataContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public DbSet<Product> Products { get; set; }
        public DbSet<CommissionRule> CommissionRules { get; set; }
        public DbSet<FlatCommissionRule> FlatCommissionRules { get; set; }
        public DbSet<TireCommisionRule> TireCommissionRules { get; set; }
        public DbSet<PercentageCommisionRule> PercentageCommissionRules { get; set; }
        public DbSet<CapCommissionRule> CapCommissionRules { get; set; }
    }
}