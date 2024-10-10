using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommissionX.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.Data.ModelBuilders
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Table name
            builder.ToTable("Products");

            builder.HasKey(o => o.Id);

            builder.Property(mi => mi.Price)
             .HasColumnType("decimal(18,2)") // Ensure correct decimal precision
             .IsRequired();

            builder.Property(o => o.Name).HasMaxLength(200).IsRequired(false);

        }
    }
}