using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class NumberGuess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "80462a9b-d956-498f-8273-92d1cd8b2b6b", "2d4ce928-d755-4965-9487-64dcdc75beb4" });

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Minigames",
                keyColumn: "MinigameID",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80462a9b-d956-498f-8273-92d1cd8b2b6b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d4ce928-d755-4965-9487-64dcdc75beb4");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 55);

            migrationBuilder.AddColumn<string>(
                name: "Number1",
                table: "Minigames",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number2",
                table: "Minigames",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a80ea326-efd8-49dc-b275-fa030ef75e16", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "51c7c7ad-297f-485c-bb58-d7f322a65df2", 0, "e1dfb8e8-5397-4775-9dfd-efe876407504", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEEQQg7MpZqwp2Em5Ezv6+CFRAlG0OOTgZVDzoSVkDohmdzfGjjHXwdHr4drqQjxfRg==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemID", "Description", "Effect", "IsDrinkable", "Name", "PlayerID", "Price" },
                values: new object[] { 7, "A strange crystal that pulses with an otherworldly energy.", "{}", false, "Mysterious Crystal", null, 0 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a80ea326-efd8-49dc-b275-fa030ef75e16", "51c7c7ad-297f-485c-bb58-d7f322a65df2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a80ea326-efd8-49dc-b275-fa030ef75e16", "51c7c7ad-297f-485c-bb58-d7f322a65df2" });

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a80ea326-efd8-49dc-b275-fa030ef75e16");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51c7c7ad-297f-485c-bb58-d7f322a65df2");

            migrationBuilder.DropColumn(
                name: "Number1",
                table: "Minigames");

            migrationBuilder.DropColumn(
                name: "Number2",
                table: "Minigames");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "80462a9b-d956-498f-8273-92d1cd8b2b6b", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2d4ce928-d755-4965-9487-64dcdc75beb4", 0, "4f9d9387-4746-480a-b6f2-778ddee0e648", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEOESIwbAAguuKPMLbedb2LgCGil7HrqCujU5cLLnoVIGoHqgUxdFQXGo0cH9do0gKA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemID", "Description", "Effect", "IsDrinkable", "Name", "PlayerID", "Price" },
                values: new object[] { 4, "A mystical crystal that instantly restores 50 health points.", "{\"health\": 50}", true, "Healing Crystal", null, 100 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "BackgroundImageUrl", "Description", "HasMinigame", "HasRequiredItem", "HasShop", "Items", "Name" },
                values: new object[] { 55, "/images/rps-background.jpg", "A mysterious figure challenges you to a game of Rock Paper Scissors.", true, false, false, "[]", "Rock Paper Scissors Challenge" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "80462a9b-d956-498f-8273-92d1cd8b2b6b", "2d4ce928-d755-4965-9487-64dcdc75beb4" });

            migrationBuilder.InsertData(
                table: "Minigames",
                columns: new[] { "MinigameID", "Description", "LocationID", "LoseLocationID", "OpponentName", "Type", "WinLocationID" },
                values: new object[] { new Guid("550e8400-e29b-41d4-a716-446655440000"), "Challenge the mysterious stranger to a game of Rock Paper Scissors! First to 3 wins.", 55, 57, "Mysterious Stranger", "RPS", 56 });
        }
    }
}
