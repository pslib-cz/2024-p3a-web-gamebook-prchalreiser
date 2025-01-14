using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class nazdar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0aa8164c-b0e7-4bdd-b0a3-e563fbc693c7", "71d57002-82cf-4ed7-bd53-a6b5aecd36c2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0aa8164c-b0e7-4bdd-b0a3-e563fbc693c7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71d57002-82cf-4ed7-bd53-a6b5aecd36c2");

            migrationBuilder.DropColumn(
                name: "ComputerScore",
                table: "Minigames");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Minigames");

            migrationBuilder.DropColumn(
                name: "PlayerScore",
                table: "Minigames");

            migrationBuilder.CreateTable(
                name: "PlayerMinigames",
                columns: table => new
                {
                    PlayerMinigameID = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayerID = table.Column<Guid>(type: "TEXT", nullable: false),
                    MinigameID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    PlayerScore = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerScore = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMinigames", x => x.PlayerMinigameID);
                    table.ForeignKey(
                        name: "FK_PlayerMinigames_Minigames_MinigameID",
                        column: x => x.MinigameID,
                        principalTable: "Minigames",
                        principalColumn: "MinigameID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerMinigames_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3c82f15-2d96-4b13-b75a-37a3a57bdaae", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "61af0114-3739-42f8-a842-c138b9e670e5", 0, "6ab74945-232d-42d5-ad88-7e49cbcb4239", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEDsNhXr1LQEfwU+wui/ckw0uQaPHRBJBhHXPdNRcC6SZygcdrCJYdSKfkssvCd19Bw==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d3c82f15-2d96-4b13-b75a-37a3a57bdaae", "61af0114-3739-42f8-a842-c138b9e670e5" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMinigames_MinigameID",
                table: "PlayerMinigames",
                column: "MinigameID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMinigames_PlayerID",
                table: "PlayerMinigames",
                column: "PlayerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerMinigames");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d3c82f15-2d96-4b13-b75a-37a3a57bdaae", "61af0114-3739-42f8-a842-c138b9e670e5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3c82f15-2d96-4b13-b75a-37a3a57bdaae");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61af0114-3739-42f8-a842-c138b9e670e5");

            migrationBuilder.AddColumn<int>(
                name: "ComputerScore",
                table: "Minigames",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Minigames",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PlayerScore",
                table: "Minigames",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0aa8164c-b0e7-4bdd-b0a3-e563fbc693c7", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "71d57002-82cf-4ed7-bd53-a6b5aecd36c2", 0, "83950969-f762-47d6-8959-3307adc7ed9d", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEJDruD+M2h2/jz2XZGcptHY+zF2QCl7J3SpCI7t4j4WTP92u7z3wu93iTIHjqhS2lg==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Minigames",
                keyColumn: "MinigameID",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "ComputerScore", "IsCompleted", "PlayerScore" },
                values: new object[] { 0, false, 0 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0aa8164c-b0e7-4bdd-b0a3-e563fbc693c7", "71d57002-82cf-4ed7-bd53-a6b5aecd36c2" });
        }
    }
}
