using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class fsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SampleExaminationSampleAccept_SampleAccepts_SampleAcceptId",
                table: "SampleExaminationSampleAccept");

            migrationBuilder.DropForeignKey(
                name: "FK_SampleExaminationSampleAccept_SampleExaminations_SampleExaminationId",
                table: "SampleExaminationSampleAccept");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SampleExaminationSampleAccept",
                table: "SampleExaminationSampleAccept");

            migrationBuilder.RenameTable(
                name: "SampleExaminationSampleAccept",
                newName: "SampleExaminationSampleAccepts");

            migrationBuilder.RenameIndex(
                name: "IX_SampleExaminationSampleAccept_SampleExaminationId",
                table: "SampleExaminationSampleAccepts",
                newName: "IX_SampleExaminationSampleAccepts_SampleExaminationId");

            migrationBuilder.RenameIndex(
                name: "IX_SampleExaminationSampleAccept_SampleAcceptId",
                table: "SampleExaminationSampleAccepts",
                newName: "IX_SampleExaminationSampleAccepts_SampleAcceptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SampleExaminationSampleAccepts",
                table: "SampleExaminationSampleAccepts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SampleExaminationSampleAccepts_SampleAccepts_SampleAcceptId",
                table: "SampleExaminationSampleAccepts",
                column: "SampleAcceptId",
                principalTable: "SampleAccepts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SampleExaminationSampleAccepts_SampleExaminations_SampleExaminationId",
                table: "SampleExaminationSampleAccepts",
                column: "SampleExaminationId",
                principalTable: "SampleExaminations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SampleExaminationSampleAccepts_SampleAccepts_SampleAcceptId",
                table: "SampleExaminationSampleAccepts");

            migrationBuilder.DropForeignKey(
                name: "FK_SampleExaminationSampleAccepts_SampleExaminations_SampleExaminationId",
                table: "SampleExaminationSampleAccepts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SampleExaminationSampleAccepts",
                table: "SampleExaminationSampleAccepts");

            migrationBuilder.RenameTable(
                name: "SampleExaminationSampleAccepts",
                newName: "SampleExaminationSampleAccept");

            migrationBuilder.RenameIndex(
                name: "IX_SampleExaminationSampleAccepts_SampleExaminationId",
                table: "SampleExaminationSampleAccept",
                newName: "IX_SampleExaminationSampleAccept_SampleExaminationId");

            migrationBuilder.RenameIndex(
                name: "IX_SampleExaminationSampleAccepts_SampleAcceptId",
                table: "SampleExaminationSampleAccept",
                newName: "IX_SampleExaminationSampleAccept_SampleAcceptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SampleExaminationSampleAccept",
                table: "SampleExaminationSampleAccept",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SampleExaminationSampleAccept_SampleAccepts_SampleAcceptId",
                table: "SampleExaminationSampleAccept",
                column: "SampleAcceptId",
                principalTable: "SampleAccepts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SampleExaminationSampleAccept_SampleExaminations_SampleExaminationId",
                table: "SampleExaminationSampleAccept",
                column: "SampleExaminationId",
                principalTable: "SampleExaminations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
