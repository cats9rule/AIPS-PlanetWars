using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class GameMapmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Galaxy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Galaxy",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
