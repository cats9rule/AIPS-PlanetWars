using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class Planetextrasupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttackBoost",
                table: "Planet");

            migrationBuilder.DropColumn(
                name: "DefenseBoost",
                table: "Planet");

            migrationBuilder.DropColumn(
                name: "MovementBoost",
                table: "Planet");

            migrationBuilder.AddColumn<string>(
                name: "Extras",
                table: "Planet",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extras",
                table: "Planet");

            migrationBuilder.AddColumn<int>(
                name: "AttackBoost",
                table: "Planet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefenseBoost",
                table: "Planet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementBoost",
                table: "Planet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
