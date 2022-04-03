using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class mfdsgdfgdhgf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TexAgency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficialAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentCustomerBankAccountInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankMerchant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentCustomerBankAccountInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentCustomerBankAccountInfos_CurrentCustomers_CurrentCustomerId",
                        column: x => x.CurrentCustomerId,
                        principalTable: "CurrentCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentCustomerContactInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MailAddress1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailAddress2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailAddress3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentCustomerContactInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentCustomerContactInfos_CurrentCustomers_CurrentCustomerId",
                        column: x => x.CurrentCustomerId,
                        principalTable: "CurrentCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentCustomerBankAccountInfos_CurrentCustomerId",
                table: "CurrentCustomerBankAccountInfos",
                column: "CurrentCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentCustomerContactInfos_CurrentCustomerId",
                table: "CurrentCustomerContactInfos",
                column: "CurrentCustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentCustomerBankAccountInfos");

            migrationBuilder.DropTable(
                name: "CurrentCustomerContactInfos");

            migrationBuilder.DropTable(
                name: "CurrentCustomers");
        }
    }
}
