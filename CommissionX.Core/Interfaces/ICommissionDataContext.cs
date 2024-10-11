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
        public DbSet<CommissionRule> CommissionRules { get; set; }
        public DbSet<TireCommissionRuleItem> TireCommisionRuleItems { get; set; }
    }
}