using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class GameCodeupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Session");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Session",
                newName: "GameCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameCode",
                table: "Session",
                newName: "Password");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Session",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
