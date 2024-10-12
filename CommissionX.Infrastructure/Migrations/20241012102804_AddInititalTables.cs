using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommissionX.Infrastructure.Migrations
{
    public partial class AddInititalTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SalesPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPersons", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommissionRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RuleContextType = table.Column<int>(type: "int", nullable: false),
                    RateCalculationType = table.Column<int>(type: "int", nullable: false),
                    CommissionRuleType = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommissionRules_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SalesPersonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_SalesPersons_SalesPersonId",
                        column: x => x.SalesPersonId,
                        principalTable: "SalesPersons",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductCommissionRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CommissionRuleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCommissionRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCommissionRules_CommissionRules_CommissionRuleId",
                        column: x => x.CommissionRuleId,
                        principalTable: "CommissionRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCommissionRules_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SalesPersonCommissionRules",
                columns: table => new
                {
                    CommissionRuleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SalesPersonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPersonCommissionRules", x => new { x.SalesPersonId, x.CommissionRuleId });
                    table.ForeignKey(
                        name: "FK_SalesPersonCommissionRules_CommissionRules_CommissionRuleId",
                        column: x => x.CommissionRuleId,
                        principalTable: "CommissionRules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesPersonCommissionRules_SalesPersons_SalesPersonId",
                        column: x => x.SalesPersonId,
                        principalTable: "SalesPersons",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TireCommissionRuleItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TierStart = table.Column<int>(type: "int", nullable: true),
                    TierEnd = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RateCalculationType = table.Column<int>(type: "int", nullable: false),
                    RuleContextType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CommissionRuleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TireCommissionRuleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TireCommissionRuleItems_CommissionRules_CommissionRuleId",
                        column: x => x.CommissionRuleId,
                        principalTable: "CommissionRules",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InvoiceProducts",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceProducts", x => new { x.ProductId, x.InvoiceId });
                    table.ForeignKey(
                        name: "FK_InvoiceProducts_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionRules_ProductId",
                table: "CommissionRules",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProducts_InvoiceId",
                table: "InvoiceProducts",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SalesPersonId",
                table: "Invoices",
                column: "SalesPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCommissionRules_CommissionRuleId",
                table: "ProductCommissionRules",
                column: "CommissionRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCommissionRules_ProductId",
                table: "ProductCommissionRules",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesPersonCommissionRules_CommissionRuleId",
                table: "SalesPersonCommissionRules",
                column: "CommissionRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_TireCommissionRuleItems_CommissionRuleId",
                table: "TireCommissionRuleItems",
                column: "CommissionRuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceProducts");

            migrationBuilder.DropTable(
                name: "ProductCommissionRules");

            migrationBuilder.DropTable(
                name: "SalesPersonCommissionRules");

            migrationBuilder.DropTable(
                name: "TireCommissionRuleItems");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "CommissionRules");

            migrationBuilder.DropTable(
                name: "SalesPersons");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
