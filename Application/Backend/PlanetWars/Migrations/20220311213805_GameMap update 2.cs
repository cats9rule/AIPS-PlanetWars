using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class GameMapupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Columns",
                table: "GameMaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PlanetMatrix",
                table: "GameMaps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rows",
                table: "GameMaps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Columns",
                table: "GameMaps");

            migrationBuilder.DropColumn(
                name: "PlanetMatrix",
                table: "GameMaps");

            migrationBuilder.DropColumn(
                name: "Rows",
                table: "GameMaps");
        }
    }
}
