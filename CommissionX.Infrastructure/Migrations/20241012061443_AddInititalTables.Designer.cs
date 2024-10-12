﻿// <auto-generated />
using System;
using CommissionX.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommissionX.Infrastructure.Migrations
{
    [DbContext(typeof(CommissionDataContext))]
    [Migration("20241012061443_AddInititalTables")]
    partial class AddInititalTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CommissionX.Core.Entities.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("SalesPersonId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("SalesPersonId");

                    b.ToTable("Invoices", (string)null);
                });

            modelBuilder.Entity("CommissionX.Core.Entities.InvoiceProduct", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "InvoiceId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceProducts", (string)null);
                });

            modelBuilder.Entity("CommissionX.Core.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("CommissionX.Core.Entities.Rules.CommissionRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("CommissionRuleType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<int>("RateCalculationType")
                        .HasColumnType("int");

                    b.Property<int>("RuleContextType")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("CommissionRules", (string)null);
                });

            modelBuilder.Entity("CommissionX.Core.Entities.Rules.TireCommissionRuleItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CommissionRuleId")
                        .HasColumnType("char(36)");

                    b.Property<int>("RateCalculationType")
                        .HasColumnType("int");

                    b.Property<string>("RuleContextType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("TierEnd")
                        .HasColumnType("int");

                    b.Property<int?>("TierStart")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CommissionRuleId");

                    b.ToTable("TireCommissionRuleItems", (string)null);
                });

            modelBuilder.Entity("CommissionX.Core.Entities.SalesPerson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("SalesPersons", (string)null);
                });

            modelBuilder.Entity("CommissionX.Core.Entities.SalesPersonCommissionRule", b =>
                {
                    b.Property<Guid>("SalesPersonId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CommissionRuleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.HasKey("SalesPersonId", "CommissionRuleId");

                    b.HasIndex("CommissionRuleId");

                    b.ToTable("SalesPersonCommissionRules", (string)null);
                });

            modelBuilder.Entity("CommissionX.Core.Entities.Invoice", b =>
                {
                    b.HasOne("CommissionX.Core.Entities.SalesPerson", "SalesPerson")
                        .WithMany("Invoices")
                        .HasForeignKey("SalesPersonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("SalesPerson");
                });

            modelBuilder.Entity("CommissionX.Core.Entities.InvoiceProduct", b =>
                {
                    b.HasOne("CommissionX.Core.Entities.Invoice", "Invoice")
                        .WithMany("InvoiceProducts")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CommissionX.Core.Entities.Product", "Product")
                        .WithMany("InvoiceProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CommissionX.Core.Entities.Rules.TireCommissionRuleItem", b =>
                {
                    b.HasOne("CommissionX.Core.Entities.Rules.CommissionRule", "CommissionRule")
                        .WithMany("TireCommissionRuleItems")
                        .HasForeignKey("CommissionRuleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CommissionRule");
                });

            modelBuilder.Entity("CommissionX.Core.Entities.SalesPersonCommissionRule", b =>
                {
                    b.HasOne("CommissionX.Core.Entities.Rules.CommissionRule", "CommissionRule")
                        .WithMany("SalesPersonCommissionRules")
                        .HasForeignKey("CommissionRuleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CommissionX.Core.Entities.SalesPerson", "SalesPerson")
                        .WithMany("SalesPersonCommissionRules")
                        .HasForeignKey("SalesPersonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CommissionRule");

                    b.Navigation("SalesPerson");
                });

            modelBuilder.Entity("CommissionX.Core.Entities.Invoice", b =>
                {
                    b.Navigation("InvoiceProducts");
                });

            modelBuilder.Entity("CommissionX.Core.Entities.Product", b =>
                {
                    b.Navigation("InvoiceProducts");
                });

            modelBuilder.Entity("CommissionX.Core.Entities.Rules.CommissionRule", b =>
                {
                    b.Navigation("SalesPersonCommissionRules");

                    b.Navigation("TireCommissionRuleItems");
                });

            modelBuilder.Entity("CommissionX.Core.Entities.SalesPerson", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("SalesPersonCommissionRules");
                });
#pragma warning restore 612, 618
        }
    }
}
