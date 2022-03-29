using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EighthGenerationCompetitive.Api.Migrations
{
    public partial class TraceableApplicationContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ApplicationContact",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ApplicationContact",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ApplicationContact");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ApplicationContact");
        }
    }
}