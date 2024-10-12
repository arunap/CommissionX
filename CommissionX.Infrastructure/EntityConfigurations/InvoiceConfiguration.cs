using CommissionX.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.EntityConfigurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .IsRequired();

            builder.Property(i => i.Date)
                .IsRequired();

            builder.Property(i => i.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            // Configure the foreign key relationship to SalesPerson
            builder.HasOne(i => i.SalesPerson)
                .WithMany(sp => sp.Invoices)
                .HasForeignKey(i => i.SalesPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure the one-to-many relationship between Invoice and InvoiceProduct
            builder.HasMany(i => i.InvoiceProducts)
                .WithOne(ip => ip.Invoice)
                .HasForeignKey(ip => ip.InvoiceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}