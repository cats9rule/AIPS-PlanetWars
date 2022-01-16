using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class Relationshipsupdate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galaxy_Session_SessionID",
                table: "Galaxy");

            migrationBuilder.AddForeignKey(
                name: "FK_Galaxy_Session_SessionID",
                table: "Galaxy",
                column: "SessionID",
                principalTable: "Session",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galaxy_Session_SessionID",
                table: "Galaxy");

            migrationBuilder.AddForeignKey(
                name: "FK_Galaxy_Session_SessionID",
                table: "Galaxy",
                column: "SessionID",
                principalTable: "Session",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
