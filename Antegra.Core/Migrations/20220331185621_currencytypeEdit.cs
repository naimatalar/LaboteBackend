using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class currencytypeEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrenyType",
                table: "SampleExaminationPriceCurrencies",
                newName: "CurrencyType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrencyType",
                table: "SampleExaminationPriceCurrencies",
                newName: "CurrenyType");
        }
    }
}
