using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class OldEntityrelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_PlayerColor_PlayerColorID",
                table: "Player");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerColorID",
                table: "Player",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "PlanetPlanet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Player_PlayerColor_PlayerColorID",
                table: "Player",
                column: "PlayerColorID",
                principalTable: "PlayerColor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_PlayerColor_PlayerColorID",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "PlanetPlanet");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerColorID",
                table: "Player",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_PlayerColor_PlayerColorID",
                table: "Player",
                column: "PlayerColorID",
                principalTable: "PlayerColor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
