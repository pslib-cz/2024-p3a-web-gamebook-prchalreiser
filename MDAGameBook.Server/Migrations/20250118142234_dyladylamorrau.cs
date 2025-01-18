using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class dyladylamorrau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "13a55dc5-3cbc-467c-8a07-20c7cdec357e", "190e5b36-56d6-4a28-aa02-1eaf62d2f470" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13a55dc5-3cbc-467c-8a07-20c7cdec357e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "190e5b36-56d6-4a28-aa02-1eaf62d2f470");

            migrationBuilder.CreateTable(
                name: "PlayerItems",
                columns: table => new
                {
                    PlayerItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerItems", x => x.PlayerItemId);
                    table.ForeignKey(
                        name: "FK_PlayerItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerItems_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
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
                table: "Players",
                keyColumn: "PlayerID",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Coins",
                value: 50);

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "112797ca-ed1d-4fab-9a06-65d41f77ae20", "a513461e-ee3c-4b4b-b98a-30ef381248b8" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerItems_ItemId",
                table: "PlayerItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerItems_PlayerId",
                table: "PlayerItems",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerItems");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13a55dc5-3cbc-467c-8a07-20c7cdec357e", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "190e5b36-56d6-4a28-aa02-1eaf62d2f470", 0, "db988e12-83c4-448b-ba9d-51e3d1f04e37", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEPrioEG+d7kRJsd9kLGuyICIsydTV0SCfRok5HeMh6Y057Vxwn/H5Jv4YiHwUDRC/A==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerID",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Coins",
                value: 0);

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "13a55dc5-3cbc-467c-8a07-20c7cdec357e", "190e5b36-56d6-4a28-aa02-1eaf62d2f470" });
        }
    }
}
