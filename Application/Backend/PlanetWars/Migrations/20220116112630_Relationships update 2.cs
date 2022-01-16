using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class Relationshipsupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Session_SessionID1",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Session_SessionID2",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_SessionID1",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_SessionID2",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "SessionID1",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "SessionID2",
                table: "Player");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorID",
                table: "Session",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "CurrentTurnIndex",
                table: "Session",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "CurrentTurnIndex",
                table: "Session");

            migrationBuilder.AddColumn<Guid>(
                name: "SessionID1",
                table: "Player",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SessionID2",
                table: "Player",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_SessionID1",
                table: "Player",
                column: "SessionID1",
                unique: true,
                filter: "[SessionID1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Player_SessionID2",
                table: "Player",
                column: "SessionID2",
                unique: true,
                filter: "[SessionID2] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Session_SessionID1",
                table: "Player",
                column: "SessionID1",
                principalTable: "Session",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Session_SessionID2",
                table: "Player",
                column: "SessionID2",
                principalTable: "Session",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
