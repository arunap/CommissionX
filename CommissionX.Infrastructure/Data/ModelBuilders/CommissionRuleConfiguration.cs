using CommissionX.Core.Entities.Comissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.Data.ModelBuilders
{
    public class CommissionRuleConfiguration : IEntityTypeConfiguration<CommissionRule>
    {
        public void Configure(EntityTypeBuilder<CommissionRule> builder)
        {
            // Table name
            builder.ToTable("CommissionRules");

            // Primary key
            builder.HasKey(cr => cr.Id);

            builder.Property(mi => mi.Name)
               .HasMaxLength(100)
               .IsRequired();


            // Navigation property configurations
            // builder.HasMany(cr => cr.TireCommissionRules)
            //     .WithOne(tc => tc.CommissionRule) // Assuming TireCommisionRule has a CommissionRule navigation property
            //     .HasForeignKey(tc => tc.CommissionRuleId)
            //     .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as necessary

            builder.HasMany(cr => cr.FlatCommissionRules)
                .WithOne(fc => fc.CommissionRule) // Assuming FlatCommissionRule has a CommissionRule navigation property
                .HasForeignKey(fc => fc.CommissionRuleId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as necessary

            builder.HasMany(cr => cr.PercentageCommissionRules)
                .WithOne(pc => pc.CommissionRule) // Assuming PercentageCommisionRule has a CommissionRule navigation property
                .HasForeignKey(pc => pc.CommissionRuleId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as necessary

            builder.HasMany(cr => cr.CapCommissionRules)
                .WithOne(cc => cc.CommissionRule) // Assuming CapCommisionRule has a CommissionRule navigation property
                .HasForeignKey(cc => cc.CommissionRuleId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as necessary
        }
    }
}