using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class fsdggggfdhfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveyToLaboratoeyDate",
                table: "SampleAccepts",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveyToLaboratoeyDate",
                table: "SampleAccepts");
        }
    }
}
