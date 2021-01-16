using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vtb.InvoicesService.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoicePaymentDeadlineToken = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TemplateVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DraftCreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentDeadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrintoutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceNumber_OrderingNumber = table.Column<int>(type: "int", nullable: true),
                    InvoiceNumber_Year = table.Column<int>(type: "int", nullable: true),
                    InvoiceNumber_Month = table.Column<int>(type: "int", nullable: true),
                    InvoiceNumber_Day = table.Column<int>(type: "int", nullable: true),
                    InvoiceNumber_FormattedNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalculationDirection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePosition",
                columns: table => new
                {
                    InvoicePositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrdinalNumber = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(17,4)", precision: 17, scale: 4, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(17,4)", precision: 17, scale: 4, nullable: false),
                    TaxLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxMultiplier = table.Column<decimal>(type: "decimal(4,3)", precision: 4, scale: 3, nullable: true),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePosition", x => x.InvoicePositionId);
                    table.ForeignKey(
                        name: "FK_InvoicePosition_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePosition_InvoiceId",
                table: "InvoicePosition",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicePosition");

            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
