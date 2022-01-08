using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanetWars.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Galaxy",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galaxy", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PlayerColor",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TurnIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerColor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayerColor_Color_ColorID",
                        column: x => x.ColorID,
                        principalTable: "Color",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Planet",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ArmyCount = table.Column<int>(type: "int", nullable: false),
                    MovementBoost = table.Column<int>(type: "int", nullable: false),
                    AttackBoost = table.Column<int>(type: "int", nullable: false),
                    DefenseBoost = table.Column<int>(type: "int", nullable: false),
                    GalaxyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlanetID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Planet_Galaxy_GalaxyID",
                        column: x => x.GalaxyID,
                        principalTable: "Galaxy",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planet_Planet_PlanetID",
                        column: x => x.PlanetID,
                        principalTable: "Planet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlayerOnTurnID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GalaxyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Session_Galaxy_GalaxyID",
                        column: x => x.GalaxyID,
                        principalTable: "Galaxy",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ColorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SessionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Player_PlayerColor_ColorID",
                        column: x => x.ColorID,
                        principalTable: "PlayerColor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Player_Session_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Session",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Player_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Planet_GalaxyID",
                table: "Planet",
                column: "GalaxyID");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_OwnerID",
                table: "Planet",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_PlanetID",
                table: "Planet",
                column: "PlanetID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_ColorID",
                table: "Player",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_SessionID",
                table: "Player",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_UserID",
                table: "Player",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerColor_ColorID",
                table: "PlayerColor",
                column: "ColorID");

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
                name: "FK_Planet_Player_OwnerID",
                table: "Planet",
                column: "OwnerID",
                principalTable: "Player",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Planet");

            migrationBuilder.DropTable(
                name: "Galaxy");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "PlayerColor");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Color");
        }
    }
}
