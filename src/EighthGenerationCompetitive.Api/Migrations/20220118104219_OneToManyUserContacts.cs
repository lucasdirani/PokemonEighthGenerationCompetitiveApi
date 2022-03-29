using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EighthGenerationCompetitive.Api.Migrations
{
    public partial class OneToManyUserContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserContact");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "ApplicationContact",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationContact_ApplicationUserId",
                table: "ApplicationContact",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationContact_AspNetUsers_ApplicationUserId",
                table: "ApplicationContact",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationContact_AspNetUsers_ApplicationUserId",
                table: "ApplicationContact");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationContact_ApplicationUserId",
                table: "ApplicationContact");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ApplicationContact");

            migrationBuilder.CreateTable(
                name: "ApplicationUserContact",
                columns: table => new
                {
                    ApplicationContactsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserContact", x => new { x.ApplicationContactsId, x.ApplicationUsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserContact_ApplicationContact_ApplicationContactsId",
                        column: x => x.ApplicationContactsId,
                        principalTable: "ApplicationContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserContact_AspNetUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserContact_ApplicationUsersId",
                table: "ApplicationUserContact",
                column: "ApplicationUsersId");
        }
    }
}