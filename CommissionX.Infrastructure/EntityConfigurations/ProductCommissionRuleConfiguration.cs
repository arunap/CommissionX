using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommissionX.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.EntityConfigurations
{
    // public class ProductCommissionRuleConfiguration : IEntityTypeConfiguration<ProductCommissionRule>
    // {
    //     public void Configure(EntityTypeBuilder<ProductCommissionRule> builder)
    //     {
    //         builder.ToTable("ProductCommissionRules");

    //         builder.Property(x => x.Id).ValueGeneratedOnAdd();

    //         builder.HasKey(ip => new { ip.ProductId, ip.CommissionRuleId });

    //         builder.HasOne(ip => ip.Product)
    //              .WithMany(i => i.ProductCommissionRules)
    //              .HasForeignKey(ip => ip.ProductId)
    //              .OnDelete(DeleteBehavior.NoAction);

    //         builder.HasOne(ip => ip.CommissionRule)
    //             .WithMany(p => p.ProductCommissionRules)
    //             .HasForeignKey(ip => ip.CommissionRuleId)
    //             .OnDelete(DeleteBehavior.NoAction);
    //     }
    // }
}