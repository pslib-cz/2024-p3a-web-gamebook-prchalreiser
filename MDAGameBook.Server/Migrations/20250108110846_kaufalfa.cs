using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class kaufalfa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shops_LocationID",
                table: "Shops");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1b2dfed5-cb5f-4d3a-963f-6178641cfa50", "be73ab8d-bbea-4f1d-bd92-74035825c34d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b2dfed5-cb5f-4d3a-963f-6178641cfa50");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "be73ab8d-bbea-4f1d-bd92-74035825c34d");

            migrationBuilder.AddColumn<bool>(
                name: "HasShop",
                table: "Locations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ShopItem",
                columns: table => new
                {
                    ShopItemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemID = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ShopID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopItem", x => x.ShopItemID);
                    table.ForeignKey(
                        name: "FK_ShopItem_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopItem_Shops_ShopID",
                        column: x => x.ShopID,
                        principalTable: "Shops",
                        principalColumn: "ShopID");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "69a30c21-4d16-4fc3-bdac-62074c489a26", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "54047b4b-f3e0-4390-9ed5-5428f098a29c", 0, "9ad223e7-a061-41bb-a96a-5bed0d1aa7ed", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEBJEhNbQD+hcz127UpdoOkH3Pe/ET+hmh1WDR9repGcy47RPH/8lb4VRnSuwegubyQ==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemID", "Description", "Effect", "FightMinigameMinigameID", "IsDrinkable", "Name", "PlayerID", "Price" },
                values: new object[,]
                {
                    { 2, "Restores 25 health points.", "{\"health\": 25}", null, true, "Health Potion", null, 30 },
                    { 3, "Restores 20 stamina points.", "{\"stamina\": 20}", null, true, "Stamina Boost", null, 20 }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "BackgroundImageUrl", "Description", "HasRequiredItem", "HasShop", "Items", "Name" },
                values: new object[] { 69, "/images/shop.jpg", "A cozy shop filled with various items and potions. The merchant eyes you carefully from behind the counter.", false, true, "[]", "Merchant's Corner" });

            migrationBuilder.InsertData(
                table: "ShopItem",
                columns: new[] { "ShopItemID", "ItemID", "Price", "Quantity", "ShopID" },
                values: new object[] { 1, 1, 50, 1, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "69a30c21-4d16-4fc3-bdac-62074c489a26", "54047b4b-f3e0-4390-9ed5-5428f098a29c" });

            migrationBuilder.InsertData(
                table: "ShopItem",
                columns: new[] { "ShopItemID", "ItemID", "Price", "Quantity", "ShopID" },
                values: new object[,]
                {
                    { 2, 2, 30, 5, null },
                    { 3, 3, 20, 10, null }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "ShopID", "ItemsForSale", "LocationID" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "[]", 69 });

            migrationBuilder.CreateIndex(
                name: "IX_Shops_LocationID",
                table: "Shops",
                column: "LocationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_ItemID",
                table: "ShopItem",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_ShopID",
                table: "ShopItem",
                column: "ShopID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopItem");

            migrationBuilder.DropIndex(
                name: "IX_Shops_LocationID",
                table: "Shops");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "69a30c21-4d16-4fc3-bdac-62074c489a26", "54047b4b-f3e0-4390-9ed5-5428f098a29c" });

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "ShopID",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69a30c21-4d16-4fc3-bdac-62074c489a26");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54047b4b-f3e0-4390-9ed5-5428f098a29c");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 69);

            migrationBuilder.DropColumn(
                name: "HasShop",
                table: "Locations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1b2dfed5-cb5f-4d3a-963f-6178641cfa50", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "be73ab8d-bbea-4f1d-bd92-74035825c34d", 0, "e01e1d13-7def-4059-90b1-c35668b15552", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEBUjVQBT279fmsavqjt0OG9xgP/NF3DFpMWtccZklObdD7yDm2JkQxT4c+y0A/EU8g==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1b2dfed5-cb5f-4d3a-963f-6178641cfa50", "be73ab8d-bbea-4f1d-bd92-74035825c34d" });

            migrationBuilder.CreateIndex(
                name: "IX_Shops_LocationID",
                table: "Shops",
                column: "LocationID");
        }
    }
}
