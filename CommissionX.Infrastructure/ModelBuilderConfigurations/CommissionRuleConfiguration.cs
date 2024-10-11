using CommissionX.Core.Entities.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.ModelBuilderConfigurations
{
    public class CommissionRuleConfiguration : IEntityTypeConfiguration<CommissionRule>
    {
        public void Configure(EntityTypeBuilder<CommissionRule> builder)
        {
            // Table name
            builder.ToTable("CommissionRules");

            // Primary key
            builder.HasKey(cr => cr.Id);

            builder.Property(mi => mi.ProductId).IsRequired(false);

            builder.Property(mi => mi.Name).HasMaxLength(100).IsRequired();

            builder.Property(mi => mi.Value).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(mi => mi.RuleContextType).HasConversion<string>().IsRequired();

            builder.Property(mi => mi.RateCalculationType).HasConversion<string>().IsRequired();

            builder.Property(mi => mi.CommissionRuleType).HasConversion<string>().IsRequired();
        }
    }
}