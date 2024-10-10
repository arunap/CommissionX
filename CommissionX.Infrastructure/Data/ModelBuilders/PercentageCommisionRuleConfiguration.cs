using CommissionX.Core.Entities.Comissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.Data.ModelBuilders
{
    public class PercentageCommisionRuleConfiguration : BaseRuleEntityConfiguration<PercentageCommisionRule>
    {
        public override void ConfigureSpecific(EntityTypeBuilder<PercentageCommisionRule> builder)
        {
            // Table name
            builder.ToTable("PercentageCommisionRules");

            builder.Property(mi => mi.Value)
               .HasColumnType("decimal(18,2)") // Ensure correct decimal precision
               .IsRequired();

            builder
                .HasOne(fcr => fcr.CommissionRule)
                .WithMany(cr => cr.PercentageCommissionRules) // Assuming CommissionRule has a collection of FlatCommissionRules
                .HasForeignKey(fcr => fcr.CommissionRuleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}