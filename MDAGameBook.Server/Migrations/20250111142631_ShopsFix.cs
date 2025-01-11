using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class ShopsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "937c303e-a51c-4906-9dff-f534bbc050b8", "89ff1b18-68a9-4072-971f-5c7a0d03b4d4" });

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "937c303e-a51c-4906-9dff-f534bbc050b8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89ff1b18-68a9-4072-971f-5c7a0d03b4d4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6bf7bcf7-2c2b-4096-b853-d7a56867ea4b", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "311301f7-eddf-41f0-bd78-6dcfb79c0b2d", 0, "639fd0cc-d5e2-4250-9bed-5395b69d368e", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEG0lHYyhrcpO9QNpKuDlTOC2gRan7AT+zh6DRVRBP97eSr7URt6vu2+nU4EZJE3vqg==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6bf7bcf7-2c2b-4096-b853-d7a56867ea4b", "311301f7-eddf-41f0-bd78-6dcfb79c0b2d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6bf7bcf7-2c2b-4096-b853-d7a56867ea4b", "311301f7-eddf-41f0-bd78-6dcfb79c0b2d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6bf7bcf7-2c2b-4096-b853-d7a56867ea4b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "311301f7-eddf-41f0-bd78-6dcfb79c0b2d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "937c303e-a51c-4906-9dff-f534bbc050b8", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "89ff1b18-68a9-4072-971f-5c7a0d03b4d4", 0, "ccad9309-4ae7-4dca-a957-98de76e77d44", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEGy5SuUbf5AMi6M6HRUWoJ0tV7/hsDRIlBEXdb/iydbE7uezCAXcsNwdD5JpQJNt5g==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "BackgroundImageUrl", "Description", "HasRequiredItem", "HasShop", "Items", "Name" },
                values: new object[] { 69, "/images/shop.jpg", "A dimly lit shop filled with mysterious potions and magical items. The merchant watches you with keen interest.", false, true, "[]", "Merchant's Corner" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "937c303e-a51c-4906-9dff-f534bbc050b8", "89ff1b18-68a9-4072-971f-5c7a0d03b4d4" });
        }
    }
}
