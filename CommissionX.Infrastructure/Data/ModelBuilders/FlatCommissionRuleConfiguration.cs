using CommissionX.Core.Entities.Comissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.Data.ModelBuilders
{
    public class FlatCommissionRuleConfiguration : BaseRuleEntityConfiguration<FlatCommissionRule>
    {
        public override void ConfigureSpecific(EntityTypeBuilder<FlatCommissionRule> builder)
        {
            // Table name
            builder.ToTable("FlatCommissionRules");

            builder.Property(mi => mi.Value)
               .HasColumnType("decimal(18,2)") // Ensure correct decimal precision
               .IsRequired();

            builder
                .HasOne(fcr => fcr.CommissionRule)
                .WithMany(cr => cr.FlatCommissionRules) // Assuming CommissionRule has a collection of FlatCommissionRules
                .HasForeignKey(fcr => fcr.CommissionRuleId)
                .OnDelete(DeleteBehavior.NoAction);

            


        }
    }
}