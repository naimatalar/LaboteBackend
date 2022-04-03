using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class revtaxagency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TexAgency",
                table: "CurrentCustomers",
                newName: "TaxAgency");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxAgency",
                table: "CurrentCustomers",
                newName: "TexAgency");
        }
    }
}
