using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class fsdgggdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SampleAccepts_AspNetUsers_ConfirmToGetLAboratoryUserId",
                table: "SampleAccepts");

            migrationBuilder.RenameColumn(
                name: "ConfirmToGetLAboratoryUserId",
                table: "SampleAccepts",
                newName: "ConfirmToGetLaboratoryUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SampleAccepts_ConfirmToGetLAboratoryUserId",
                table: "SampleAccepts",
                newName: "IX_SampleAccepts_ConfirmToGetLaboratoryUserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ConfirmToGetLaboratoryUserId",
                table: "SampleAccepts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_SampleAccepts_AspNetUsers_ConfirmToGetLaboratoryUserId",
                table: "SampleAccepts",
                column: "ConfirmToGetLaboratoryUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SampleAccepts_AspNetUsers_ConfirmToGetLaboratoryUserId",
                table: "SampleAccepts");

            migrationBuilder.RenameColumn(
                name: "ConfirmToGetLaboratoryUserId",
                table: "SampleAccepts",
                newName: "ConfirmToGetLAboratoryUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SampleAccepts_ConfirmToGetLaboratoryUserId",
                table: "SampleAccepts",
                newName: "IX_SampleAccepts_ConfirmToGetLAboratoryUserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ConfirmToGetLAboratoryUserId",
                table: "SampleAccepts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SampleAccepts_AspNetUsers_ConfirmToGetLAboratoryUserId",
                table: "SampleAccepts",
                column: "ConfirmToGetLAboratoryUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
