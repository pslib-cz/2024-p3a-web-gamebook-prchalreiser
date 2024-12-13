using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class LocationSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0d5f29c5-bd95-4e97-ac99-56e49869c556", "93107250-e569-4830-a4b1-2e409e8af533" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d5f29c5-bd95-4e97-ac99-56e49869c556");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93107250-e569-4830-a4b1-2e409e8af533");

            migrationBuilder.DropColumn(
                name: "VictoryReward",
                table: "FightMinigames");

            migrationBuilder.AddColumn<Guid>(
                name: "FightMinigameMinigameID",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6fc16aac-3572-43cd-83f3-d8720a30b3a3", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1eaba987-5223-4c46-bfb9-6eaa27d34f12", 0, "e2abe5e0-fa72-49b7-8d37-90aa70836eea", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEHzZ0bFu62kDMxMwhphnJrRkNJwRQCxG5/bBJtRyyM15mfq3Fja7DnxO1SzNtolIzQ==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "BackgroundImageUrl", "Description", "Items", "Name" },
                values: new object[,]
                {
                    { -1, "https://localhost:7260/wwwroot/Uploads/06dfd75a-1c7b-42a2-942d-ee3d48a26a0f.png", "Nacházíš se někde, kam ses neměl dostat :O", "[]", "Unlinked Location" },
                    { 420, "https://localhost:7260/wwwroot/Uploads/06dfd75a-1c7b-42a2-942d-ee3d48a26a0f.png", "Jsi v interiéru auta a dáváš hotbox.", "[]", "Hotbox" },
                    { 421, "https://localhost:7260/wwwroot/Uploads/06dfd75a-1c7b-42a2-942d-ee3d48a26a0f.png", "Vylezl jsi z auta a stojíš v temném lese", "[]", "Outside" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6fc16aac-3572-43cd-83f3-d8720a30b3a3", "1eaba987-5223-4c46-bfb9-6eaa27d34f12" });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "LinkID", "Condition", "FromLocationID", "ToLocationID" },
                values: new object[] { 69, null, 420, 421 });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_FightMinigames_FightMinigameMinigameID",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_FightMinigameMinigameID",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6fc16aac-3572-43cd-83f3-d8720a30b3a3", "1eaba987-5223-4c46-bfb9-6eaa27d34f12" });

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "LinkID",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fc16aac-3572-43cd-83f3-d8720a30b3a3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1eaba987-5223-4c46-bfb9-6eaa27d34f12");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 421);

            migrationBuilder.DropColumn(
                name: "FightMinigameMinigameID",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "VictoryReward",
                table: "FightMinigames",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0d5f29c5-bd95-4e97-ac99-56e49869c556", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "93107250-e569-4830-a4b1-2e409e8af533", 0, "dd22e8fa-ea6d-4498-b1e9-4d4f497df4c2", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEBOmCS8b2+M8QE6MM9Wu9Gdn4vJNcrEimZ5YMiLBtJooPNgIM4C8/Q1pGmUf7XuvLw==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0d5f29c5-bd95-4e97-ac99-56e49869c556", "93107250-e569-4830-a4b1-2e409e8af533" });
        }
    }
}
