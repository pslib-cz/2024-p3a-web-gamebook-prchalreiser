using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class gagnamstylke : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0718c044-bd98-4abd-ae94-12ac62941479", "31ba445d-daaf-4fff-bd7e-32aba16ee02b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0718c044-bd98-4abd-ae94-12ac62941479");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "31ba445d-daaf-4fff-bd7e-32aba16ee02b");

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

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Minigames",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0aa8164c-b0e7-4bdd-b0a3-e563fbc693c7", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "71d57002-82cf-4ed7-bd53-a6b5aecd36c2", 0, "83950969-f762-47d6-8959-3307adc7ed9d", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEJDruD+M2h2/jz2XZGcptHY+zF2QCl7J3SpCI7t4j4WTP92u7z3wu93iTIHjqhS2lg==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "BackgroundImageUrl", "Description", "HasRequiredItem", "HasShop", "Items", "Name" },
                values: new object[] { 55, "/images/rps-background.jpg", "A mysterious figure challenges you to a game of Rock Paper Scissors.", false, false, "[]", "Rock Paper Scissors Challenge" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0aa8164c-b0e7-4bdd-b0a3-e563fbc693c7", "71d57002-82cf-4ed7-bd53-a6b5aecd36c2" });

            migrationBuilder.InsertData(
                table: "Minigames",
                columns: new[] { "MinigameID", "ComputerScore", "Description", "IsCompleted", "LocationID", "PlayerScore", "Type" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), 0, "Challenge the computer to a game of Rock Paper Scissors! First to 3 wins.", false, 55, 0, "RPS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0aa8164c-b0e7-4bdd-b0a3-e563fbc693c7", "71d57002-82cf-4ed7-bd53-a6b5aecd36c2" });

            migrationBuilder.DeleteData(
                table: "Minigames",
                keyColumn: "MinigameID",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0aa8164c-b0e7-4bdd-b0a3-e563fbc693c7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71d57002-82cf-4ed7-bd53-a6b5aecd36c2");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 55);

            migrationBuilder.DropColumn(
                name: "ComputerScore",
                table: "Minigames");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Minigames");

            migrationBuilder.DropColumn(
                name: "PlayerScore",
                table: "Minigames");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Minigames");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0718c044-bd98-4abd-ae94-12ac62941479", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "31ba445d-daaf-4fff-bd7e-32aba16ee02b", 0, "fb5a51bb-25c6-45b7-88c4-228fc2f2975f", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEJX4rVHZbDpNtBJtSugxOdJVKMu+TdwtI3U/c/wwFI8LdpG7ov/5v7gMZa3H5wsC6A==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0718c044-bd98-4abd-ae94-12ac62941479", "31ba445d-daaf-4fff-bd7e-32aba16ee02b" });
        }
    }
}
