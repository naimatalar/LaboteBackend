using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class fffsdfsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserTopicId",
                table: "Laboratories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Laboratories_UserTopicId",
                table: "Laboratories",
                column: "UserTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laboratories_UserTopics_UserTopicId",
                table: "Laboratories",
                column: "UserTopicId",
                principalTable: "UserTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laboratories_UserTopics_UserTopicId",
                table: "Laboratories");

            migrationBuilder.DropIndex(
                name: "IX_Laboratories_UserTopicId",
                table: "Laboratories");

            migrationBuilder.DropColumn(
                name: "UserTopicId",
                table: "Laboratories");
        }
    }
}
