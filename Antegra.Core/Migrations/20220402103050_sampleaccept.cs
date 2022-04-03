using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class sampleaccept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SampleAccepts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufactureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UnitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LaboteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleAcceptStatus = table.Column<int>(type: "int", nullable: false),
                    SampleReturnType = table.Column<int>(type: "int", nullable: false),
                    SampleAcceptPackaging = table.Column<int>(type: "int", nullable: false),
                    SampleAcceptBringingType = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleAccepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleAccepts_AspNetUsers_LaboteUserId",
                        column: x => x.LaboteUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SampleAccepts_CurrentCustomers_CurrentCustomerId",
                        column: x => x.CurrentCustomerId,
                        principalTable: "CurrentCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SampleExaminationSampleAccept",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleAcceptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleExaminationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleExaminationSampleAccept", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleExaminationSampleAccept_SampleAccepts_SampleAcceptId",
                        column: x => x.SampleAcceptId,
                        principalTable: "SampleAccepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SampleExaminationSampleAccept_SampleExaminations_SampleExaminationId",
                        column: x => x.SampleExaminationId,
                        principalTable: "SampleExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SampleAccepts_CurrentCustomerId",
                table: "SampleAccepts",
                column: "CurrentCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleAccepts_LaboteUserId",
                table: "SampleAccepts",
                column: "LaboteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleExaminationSampleAccept_SampleAcceptId",
                table: "SampleExaminationSampleAccept",
                column: "SampleAcceptId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleExaminationSampleAccept_SampleExaminationId",
                table: "SampleExaminationSampleAccept",
                column: "SampleExaminationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SampleExaminationSampleAccept");

            migrationBuilder.DropTable(
                name: "SampleAccepts");
        }
    }
}
