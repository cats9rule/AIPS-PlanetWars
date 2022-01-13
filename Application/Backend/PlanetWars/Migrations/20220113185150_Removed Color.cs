using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class RemovedColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_PlayerColor_ColorID",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerColor_Color_ColorID",
                table: "PlayerColor");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropIndex(
                name: "IX_PlayerColor_ColorID",
                table: "PlayerColor");

            migrationBuilder.DropColumn(
                name: "ColorID",
                table: "PlayerColor");

            migrationBuilder.RenameColumn(
                name: "ColorID",
                table: "Player",
                newName: "PlayerColorID");

            migrationBuilder.RenameIndex(
                name: "IX_Player_ColorID",
                table: "Player",
                newName: "IX_Player_PlayerColorID");

            migrationBuilder.AddColumn<string>(
                name: "ColorHexValue",
                table: "PlayerColor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_PlayerColor_PlayerColorID",
                table: "Player",
                column: "PlayerColorID",
                principalTable: "PlayerColor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_PlayerColor_PlayerColorID",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "ColorHexValue",
                table: "PlayerColor");

            migrationBuilder.RenameColumn(
                name: "PlayerColorID",
                table: "Player",
                newName: "ColorID");

            migrationBuilder.RenameIndex(
                name: "IX_Player_PlayerColorID",
                table: "Player",
                newName: "IX_Player_ColorID");

            migrationBuilder.AddColumn<Guid>(
                name: "ColorID",
                table: "PlayerColor",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HexValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerColor_ColorID",
                table: "PlayerColor",
                column: "ColorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_PlayerColor_ColorID",
                table: "Player",
                column: "ColorID",
                principalTable: "PlayerColor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerColor_Color_ColorID",
                table: "PlayerColor",
                column: "ColorID",
                principalTable: "Color",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
