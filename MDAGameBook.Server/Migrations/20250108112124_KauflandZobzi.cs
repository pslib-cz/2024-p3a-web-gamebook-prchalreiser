using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class KauflandZobzi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dcb6bc48-06de-44f4-9f25-b73dff7ebe38", "899e3594-07ad-484b-b2d8-31a8339ef3c8" });

            migrationBuilder.DeleteData(
                table: "ShopItem",
                keyColumn: "ShopItemID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShopItem",
                keyColumn: "ShopItemID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ShopItem",
                keyColumn: "ShopItemID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dcb6bc48-06de-44f4-9f25-b73dff7ebe38");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "899e3594-07ad-484b-b2d8-31a8339ef3c8");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34314760-5a81-471b-8f8b-0bddd278aa53", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4822405e-553a-4a39-b444-5f4e572cf489", 0, "576b7e76-a34f-4fb0-9b97-f1d3d426f340", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEKpHOrHavaxoBosTCr+wC3524UYu8BapQ/sP1/zDiemilglsEPiZSYu3fvRWtBzjZg==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemID", "Description", "Effect", "FightMinigameMinigameID", "IsDrinkable", "Name", "PlayerID", "Price" },
                values: new object[,]
                {
                    { 4, "A mystical crystal that instantly restores 50 health points.", "{\"health\": 50}", null, true, "Healing Crystal", null, 100 },
                    { 5, "A fizzy drink that restores 40 stamina points.", "{\"stamina\": 40}", null, true, "Energy Drink", null, 75 },
                    { 6, "A bitter potion that reduces withdrawal effects by 30 points.", "{\"withdrawal\": -30}", null, true, "Anti-Withdrawal Potion", null, 150 }
                });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 69,
                column: "Description",
                value: "A dimly lit shop filled with mysterious potions and magical items. The merchant watches you with keen interest.");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "34314760-5a81-471b-8f8b-0bddd278aa53", "4822405e-553a-4a39-b444-5f4e572cf489" });

            migrationBuilder.InsertData(
                table: "ShopItem",
                columns: new[] { "ShopItemID", "ItemID", "Price", "Quantity", "ShopID" },
                values: new object[,]
                {
                    { 4, 4, 100, 3, null },
                    { 5, 5, 75, 5, null },
                    { 6, 6, 150, 2, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "34314760-5a81-471b-8f8b-0bddd278aa53", "4822405e-553a-4a39-b444-5f4e572cf489" });

            migrationBuilder.DeleteData(
                table: "ShopItem",
                keyColumn: "ShopItemID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ShopItem",
                keyColumn: "ShopItemID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ShopItem",
                keyColumn: "ShopItemID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34314760-5a81-471b-8f8b-0bddd278aa53");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4822405e-553a-4a39-b444-5f4e572cf489");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 6);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dcb6bc48-06de-44f4-9f25-b73dff7ebe38", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "899e3594-07ad-484b-b2d8-31a8339ef3c8", 0, "325d926e-d281-4872-9317-e09586c37719", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEOAgI2cedksQ/j6lVTEv0FGEltRbmD7j3ZkLvKdfku2dpStNfE9U4WKp5m/f+0gd2g==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemID", "Description", "Effect", "FightMinigameMinigameID", "IsDrinkable", "Name", "PlayerID", "Price" },
                values: new object[,]
                {
                    { 2, "Restores 25 health points.", "{\"health\": 25}", null, true, "Health Potion", null, 30 },
                    { 3, "Restores 20 stamina points.", "{\"stamina\": 20}", null, true, "Stamina Boost", null, 20 }
                });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 69,
                column: "Description",
                value: "A cozy shop filled with various items and potions. The merchant eyes you carefully from behind the counter.");

            migrationBuilder.InsertData(
                table: "ShopItem",
                columns: new[] { "ShopItemID", "ItemID", "Price", "Quantity", "ShopID" },
                values: new object[] { 1, 1, 50, 1, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dcb6bc48-06de-44f4-9f25-b73dff7ebe38", "899e3594-07ad-484b-b2d8-31a8339ef3c8" });

            migrationBuilder.InsertData(
                table: "ShopItem",
                columns: new[] { "ShopItemID", "ItemID", "Price", "Quantity", "ShopID" },
                values: new object[,]
                {
                    { 2, 2, 30, 5, null },
                    { 3, 3, 20, 10, null }
                });
        }
    }
}
