using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    RoomName = table.Column<string>(type: "text", nullable: false),
                    RoomStatus = table.Column<int>(type: "integer", nullable: false),
                    TotalUser = table.Column<int>(type: "integer", nullable: false),
                    HostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRoom_User_HostId",
                        column: x => x.HostId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameRoomUser",
                columns: table => new
                {
                    GameRoomsParticipationId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoomUser", x => new { x.GameRoomsParticipationId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GameRoomUser_GameRoom_GameRoomsParticipationId",
                        column: x => x.GameRoomsParticipationId,
                        principalTable: "GameRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameRoomUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameStatistic",
                columns: table => new
                {
                    GameRoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoserId = table.Column<Guid>(type: "uuid", nullable: false),
                    WinnerHealth = table.Column<int>(type: "integer", nullable: false),
                    RoundCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatistic", x => x.GameRoomId);
                    table.ForeignKey(
                        name: "FK_GameStatistic_GameRoom_GameRoomId",
                        column: x => x.GameRoomId,
                        principalTable: "GameRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStatistic_User_LoserId",
                        column: x => x.LoserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStatistic_User_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameRoom_HostId",
                table: "GameRoom",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRoomUser_UsersId",
                table: "GameRoomUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistic_LoserId",
                table: "GameStatistic",
                column: "LoserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistic_WinnerId",
                table: "GameStatistic",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameRoomUser");

            migrationBuilder.DropTable(
                name: "GameStatistic");

            migrationBuilder.DropTable(
                name: "GameRoom");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
