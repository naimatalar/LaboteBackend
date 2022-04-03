using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class fsdggg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SampleAccepts_AspNetUsers_LaboteUserId",
                table: "SampleAccepts");

            migrationBuilder.AddColumn<Guid>(
                name: "ConfirmToGetLAboratoryUserId",
                table: "SampleAccepts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SampleAccepts_ConfirmToGetLAboratoryUserId",
                table: "SampleAccepts",
                column: "ConfirmToGetLAboratoryUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SampleAccepts_AspNetUsers_ConfirmToGetLAboratoryUserId",
                table: "SampleAccepts",
                column: "ConfirmToGetLAboratoryUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SampleAccepts_AspNetUsers_LaboteUserId",
                table: "SampleAccepts",
                column: "LaboteUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SampleAccepts_AspNetUsers_ConfirmToGetLAboratoryUserId",
                table: "SampleAccepts");

            migrationBuilder.DropForeignKey(
                name: "FK_SampleAccepts_AspNetUsers_LaboteUserId",
                table: "SampleAccepts");

            migrationBuilder.DropIndex(
                name: "IX_SampleAccepts_ConfirmToGetLAboratoryUserId",
                table: "SampleAccepts");

            migrationBuilder.DropColumn(
                name: "ConfirmToGetLAboratoryUserId",
                table: "SampleAccepts");

            migrationBuilder.AddForeignKey(
                name: "FK_SampleAccepts_AspNetUsers_LaboteUserId",
                table: "SampleAccepts",
                column: "LaboteUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
