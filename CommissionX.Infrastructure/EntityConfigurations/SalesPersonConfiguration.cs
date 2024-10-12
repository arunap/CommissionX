using CommissionX.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommissionX.Infrastructure.EntityConfigurations
{
    public class SalesPersonConfiguration : IEntityTypeConfiguration<SalesPerson>
    {
        public void Configure(EntityTypeBuilder<SalesPerson> builder)
        {
            builder.ToTable("SalesPersons");

            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.Id).IsRequired();

            builder.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasMany(c => c.SalesPersonCommissionRules)
                .WithOne(c => c.SalesPerson)
                .HasForeignKey(c => c.SalesPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(sp => sp.Invoices)
                .WithOne(i => i.SalesPerson)
                .HasForeignKey(i => i.SalesPersonId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}