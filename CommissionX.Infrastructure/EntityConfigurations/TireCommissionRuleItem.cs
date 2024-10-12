using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.EntityConfigurations
{
    public class TireCommissionRuleItem : IEntityTypeConfiguration<Core.Entities.Rules.TireCommissionRuleItem>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Rules.TireCommissionRuleItem> builder)
        {
            // Table name
            builder.ToTable("TireCommissionRuleItems");

            // Primary key
            builder.HasKey(cr => cr.Id);

            builder.Property(mi => mi.Value).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(c => c.TierStart).HasConversion<int>().IsRequired(false);

            builder.Property(c => c.TierEnd).HasConversion<int>().IsRequired(false);

            builder.Property(mi => mi.RuleContextType).HasConversion<string>().IsRequired();

            // Configure the relationship with CommissionRule
            builder.HasOne(t => t.CommissionRule)
                .WithMany(cr => cr.TireCommissionRuleItems)
                .HasForeignKey(t => t.CommissionRuleId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}