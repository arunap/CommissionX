using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.EntityConfigurations
{
    public class SalesPersonCommissionRuleConfiguration : IEntityTypeConfiguration<Core.Entities.SalesPersonCommissionRule>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.SalesPersonCommissionRule> builder)
        {
            builder.ToTable("SalesPersonCommissionRules");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasKey(ip => new { ip.SalesPersonId, ip.CommissionRuleId });

            builder.HasOne(ip => ip.SalesPerson)
                 .WithMany(i => i.SalesPersonCommissionRules)
                 .HasForeignKey(ip => ip.SalesPersonId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ip => ip.CommissionRule)
                .WithMany(p => p.SalesPersonCommissionRules)
                .HasForeignKey(ip => ip.CommissionRuleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}