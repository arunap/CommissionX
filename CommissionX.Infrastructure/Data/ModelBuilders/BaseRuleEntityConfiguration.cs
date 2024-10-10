using CommissionX.Core.Entities;
using CommissionX.Core.Entities.Comissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.Data.ModelBuilders
{
    public abstract class BaseRuleEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : CommisionBase, IBaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(o => o.Id);

            ConfigureSpecific(builder);


            builder.Property(c => c.RuleScope)
                 .HasConversion<string>()
                 .IsRequired(); // Assuming RuleScope is required

            builder.Property(c => c.ProductId)
                .IsRequired(false); // Nullable property

        }

        // Abstract method for specific configurations
        public abstract void ConfigureSpecific(EntityTypeBuilder<TEntity> builder);
    }
}