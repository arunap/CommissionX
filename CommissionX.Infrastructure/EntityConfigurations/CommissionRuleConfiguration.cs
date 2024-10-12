using CommissionX.Core.Entities.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.EntityConfigurations
{
    public class CommissionRuleConfiguration : IEntityTypeConfiguration<CommissionRule>
    {
        public void Configure(EntityTypeBuilder<CommissionRule> builder)
        {
            // Set the table name
            builder.ToTable("CommissionRules");

            // Configure the primary key
            builder.HasKey(cr => cr.Id);

            // Set properties configuration
            builder.Property(cr => cr.Id).IsRequired();

            builder.Property(cr => cr.Name).IsRequired().HasMaxLength(100);

            builder.Property(cr => cr.Value).IsRequired().HasColumnType("decimal(18, 2)");

            builder.Property(cr => cr.RuleContextType).IsRequired();

            builder.Property(cr => cr.RateCalculationType).IsRequired();

            builder.Property(cr => cr.CommissionRuleType).IsRequired();

            builder.Property(mi => mi.ProductId).IsRequired(false);
            
            // builder.HasMany(cr => cr.ProductCommissionRules)
            //     .WithOne(cr => cr.CommissionRule)
            //     .HasForeignKey(cr => cr.CommissionRuleId)
            //     .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(cr => cr.SalesPersonCommissionRules)
                .WithOne(cr => cr.CommissionRule)
                .HasForeignKey(cr => cr.CommissionRuleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
               .HasMany(cr => cr.TireCommissionRuleItems)
               .WithOne(cr => cr.CommissionRule)
               .HasForeignKey(cr => cr.CommissionRuleId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}