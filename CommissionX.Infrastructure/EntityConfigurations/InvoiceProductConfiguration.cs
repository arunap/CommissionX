using CommissionX.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.EntityConfigurations
{
    public class InvoiceProductConfiguration : IEntityTypeConfiguration<InvoiceProduct>
    {
        public void Configure(EntityTypeBuilder<InvoiceProduct> builder)
        {
            builder.ToTable("InvoiceProducts");

            builder.HasKey(ip => new { ip.ProductId, ip.InvoiceId });

            builder.HasOne(ip => ip.Invoice)
                 .WithMany(i => i.InvoiceProducts)
                 .HasForeignKey(ip => ip.InvoiceId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ip => ip.Product)
                .WithMany(p => p.InvoiceProducts)
                .HasForeignKey(ip => ip.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(ip => ip.Quantity).IsRequired();
        }
    }
}