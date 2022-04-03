using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class currencytypeEditdsfgd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SampleExaminationResultValueTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleExaminationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementUnitLongName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasureUnitType = table.Column<int>(type: "int", nullable: false),
                    MeasureUnitSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleExaminationResultValueTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleExaminationResultValueTypes_SampleExaminations_SampleExaminationId",
                        column: x => x.SampleExaminationId,
                        principalTable: "SampleExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SampleExaminationResultValueTypes_SampleExaminationId",
                table: "SampleExaminationResultValueTypes",
                column: "SampleExaminationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SampleExaminationResultValueTypes");
        }
    }
}
