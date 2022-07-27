using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class gamemapidugalaxy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GameMapID",
                table: "Galaxy",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameMapID",
                table: "Galaxy");
        }
    }
}
