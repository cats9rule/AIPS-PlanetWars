using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class PlanetPlanetfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planet_Planet_PlanetID",
                table: "Planet");

            migrationBuilder.DropIndex(
                name: "IX_Planet_PlanetID",
                table: "Planet");

            migrationBuilder.DropColumn(
                name: "PlanetID",
                table: "Planet");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Galaxy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanetCount",
                table: "Galaxy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "ResourcePlanetRatio",
                table: "Galaxy",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "PlanetPlanet",
                columns: table => new
                {
                    PlanetFromID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanetToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanetPlanet", x => new { x.PlanetFromID, x.PlanetToID });
                    table.ForeignKey(
                        name: "FK_PlanetPlanet_Planet_PlanetFromID",
                        column: x => x.PlanetFromID,
                        principalTable: "Planet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanetPlanet_Planet_PlanetToID",
                        column: x => x.PlanetToID,
                        principalTable: "Planet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanetPlanet_PlanetToID",
                table: "PlanetPlanet",
                column: "PlanetToID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanetPlanet");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Galaxy");

            migrationBuilder.DropColumn(
                name: "PlanetCount",
                table: "Galaxy");

            migrationBuilder.DropColumn(
                name: "ResourcePlanetRatio",
                table: "Galaxy");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanetID",
                table: "Planet",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planet_PlanetID",
                table: "Planet",
                column: "PlanetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Planet_Planet_PlanetID",
                table: "Planet",
                column: "PlanetID",
                principalTable: "Planet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
