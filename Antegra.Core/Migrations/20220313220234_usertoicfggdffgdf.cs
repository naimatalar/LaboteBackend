using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class usertoicfggdffgdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserTopic_UserTopicId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTopic",
                table: "UserTopic");

            migrationBuilder.RenameTable(
                name: "UserTopic",
                newName: "UserTopics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTopics",
                table: "UserTopics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserTopics_UserTopicId",
                table: "AspNetUsers",
                column: "UserTopicId",
                principalTable: "UserTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserTopics_UserTopicId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTopics",
                table: "UserTopics");

            migrationBuilder.RenameTable(
                name: "UserTopics",
                newName: "UserTopic");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTopic",
                table: "UserTopic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserTopic_UserTopicId",
                table: "AspNetUsers",
                column: "UserTopicId",
                principalTable: "UserTopic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
