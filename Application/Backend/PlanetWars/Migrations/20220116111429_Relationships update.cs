using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class Relationshipsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_Galaxy_GalaxyID",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Player_CreatorID",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Player_PlayerOnTurnID",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_CreatorID",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_GalaxyID",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_PlayerOnTurnID",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "GalaxyID",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "PlayerOnTurnID",
                table: "Session");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Player",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SessionID",
                table: "Player",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerID",
                table: "Planet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GalaxyID",
                table: "Planet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SessionID",
                table: "Galaxy",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_Galaxy_SessionID",
                table: "Galaxy",
                column: "SessionID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Galaxy_Session_SessionID",
                table: "Galaxy",
                column: "SessionID",
                principalTable: "Session",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galaxy_Session_SessionID",
                table: "Galaxy");

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

            migrationBuilder.DropIndex(
                name: "IX_Galaxy_SessionID",
                table: "Galaxy");

            migrationBuilder.DropColumn(
                name: "SessionID1",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "SessionID2",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "SessionID",
                table: "Galaxy");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorID",
                table: "Session",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GalaxyID",
                table: "Session",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerOnTurnID",
                table: "Session",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Player",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "SessionID",
                table: "Player",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerID",
                table: "Planet",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "GalaxyID",
                table: "Planet",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Session_CreatorID",
                table: "Session",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_GalaxyID",
                table: "Session",
                column: "GalaxyID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_PlayerOnTurnID",
                table: "Session",
                column: "PlayerOnTurnID");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Galaxy_GalaxyID",
                table: "Session",
                column: "GalaxyID",
                principalTable: "Galaxy",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Player_CreatorID",
                table: "Session",
                column: "CreatorID",
                principalTable: "Player",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Player_PlayerOnTurnID",
                table: "Session",
                column: "PlayerOnTurnID",
                principalTable: "Player",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
