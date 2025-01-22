using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class petergriffin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_FightMinigames_FightMinigameMinigameID",
                table: "Items");

            migrationBuilder.DropTable(
                name: "FightMinigames");

            migrationBuilder.DropIndex(
                name: "IX_Items_FightMinigameMinigameID",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "112797ca-ed1d-4fab-9a06-65d41f77ae20", "a513461e-ee3c-4b4b-b98a-30ef381248b8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "112797ca-ed1d-4fab-9a06-65d41f77ae20");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a513461e-ee3c-4b4b-b98a-30ef381248b8");

            migrationBuilder.DropColumn(
                name: "FightMinigameMinigameID",
                table: "Items");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "110b92d3-1b9c-4e98-8364-0a84a9891791", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b8847ae2-1fa7-44ae-9949-712b67599b78", 0, "e35b9abf-a98e-4d23-8fdc-d861aacc825d", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEBhSLzsoeh1k7/SgdH+IlI4Ry8zuuGyFHF0jCfNNbAae2goaXLjnYrbstZj/q7VyaA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "110b92d3-1b9c-4e98-8364-0a84a9891791", "b8847ae2-1fa7-44ae-9949-712b67599b78" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "110b92d3-1b9c-4e98-8364-0a84a9891791", "b8847ae2-1fa7-44ae-9949-712b67599b78" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "110b92d3-1b9c-4e98-8364-0a84a9891791");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b8847ae2-1fa7-44ae-9949-712b67599b78");

            migrationBuilder.AddColumn<Guid>(
                name: "FightMinigameMinigameID",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FightMinigames",
                columns: table => new
                {
                    MinigameID = table.Column<Guid>(type: "TEXT", nullable: false),
                    EnemyHealth = table.Column<int>(type: "INTEGER", nullable: false),
                    EnemyName = table.Column<string>(type: "TEXT", nullable: false),
                    EnemyStrength = table.Column<int>(type: "INTEGER", nullable: false),
                    StatPenalty = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FightMinigames", x => x.MinigameID);
                    table.ForeignKey(
                        name: "FK_FightMinigames_Minigames_MinigameID",
                        column: x => x.MinigameID,
                        principalTable: "Minigames",
                        principalColumn: "MinigameID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "112797ca-ed1d-4fab-9a06-65d41f77ae20", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a513461e-ee3c-4b4b-b98a-30ef381248b8", 0, "66a01636-17a4-47c6-99b3-cf1c0f1d91cc", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEGHatwyCRqsQED2G9HNAX8bcq3zZZCfXOqFJRKnGf11SPjej9fenliXyHH1bVeikag==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 1,
                column: "FightMinigameMinigameID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 4,
                column: "FightMinigameMinigameID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 5,
                column: "FightMinigameMinigameID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 6,
                column: "FightMinigameMinigameID",
                value: null);

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "112797ca-ed1d-4fab-9a06-65d41f77ae20", "a513461e-ee3c-4b4b-b98a-30ef381248b8" });

            migrationBuilder.CreateIndex(
                name: "IX_Items_FightMinigameMinigameID",
                table: "Items",
                column: "FightMinigameMinigameID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_FightMinigames_FightMinigameMinigameID",
                table: "Items",
                column: "FightMinigameMinigameID",
                principalTable: "FightMinigames",
                principalColumn: "MinigameID");
        }
    }
}
