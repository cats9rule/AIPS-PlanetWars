using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class updateSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerCount",
                table: "Session",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerCount",
                table: "Session");
        }
    }
}
