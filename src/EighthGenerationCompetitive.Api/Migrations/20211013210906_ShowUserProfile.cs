using Microsoft.EntityFrameworkCore.Migrations;

namespace EighthGenerationCompetitive.Api.Migrations
{
    public partial class ShowUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowProfile",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowProfile",
                table: "AspNetUsers");
        }
    }
}