using Microsoft.EntityFrameworkCore.Migrations;

namespace EighthGenerationCompetitive.Api.Migrations
{
    public partial class ApplicationContactActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ApplicationContact",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ApplicationContact");
        }
    }
}