using CommissionX.Core.Entities.Comissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.Data.ModelBuilders
{
    public class CapCommisionRuleConfiguration : BaseRuleEntityConfiguration<CapCommissionRule>
    {
        public override void ConfigureSpecific(EntityTypeBuilder<CapCommissionRule> builder)
        {
            // Table name
            builder.ToTable("CapCommisionRules");

            builder.Property(mi => mi.Value)
               .HasColumnType("decimal(18,2)") // Ensure correct decimal precision
               .IsRequired();

            builder.Property(c => c.RateType)
             .HasConversion<int>()
             .IsRequired(); // Assuming RateType is required

            builder
                .HasOne(fcr => fcr.CommissionRule)
                .WithMany(cr => cr.CapCommissionRules) // Assuming CommissionRule has a collection of FlatCommissionRules
                .HasForeignKey(fcr => fcr.CommissionRuleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}