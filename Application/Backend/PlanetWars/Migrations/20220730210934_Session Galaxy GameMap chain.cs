using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class SessionGalaxyGameMapchain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "GameMapID",
                table: "Galaxy",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Galaxy_GameMapID",
                table: "Galaxy",
                column: "GameMapID");

            migrationBuilder.AddForeignKey(
                name: "FK_Galaxy_GameMaps_GameMapID",
                table: "Galaxy",
                column: "GameMapID",
                principalTable: "GameMaps",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galaxy_GameMaps_GameMapID",
                table: "Galaxy");

            migrationBuilder.DropIndex(
                name: "IX_Galaxy_GameMapID",
                table: "Galaxy");

            migrationBuilder.AlterColumn<Guid>(
                name: "GameMapID",
                table: "Galaxy",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
