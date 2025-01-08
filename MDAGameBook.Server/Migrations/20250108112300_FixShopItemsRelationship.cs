using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixShopItemsRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopItem_Shops_ShopID",
                table: "ShopItem");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "34314760-5a81-471b-8f8b-0bddd278aa53", "4822405e-553a-4a39-b444-5f4e572cf489" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34314760-5a81-471b-8f8b-0bddd278aa53");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4822405e-553a-4a39-b444-5f4e572cf489");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShopID",
                table: "ShopItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "937c303e-a51c-4906-9dff-f534bbc050b8", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "89ff1b18-68a9-4072-971f-5c7a0d03b4d4", 0, "ccad9309-4ae7-4dca-a957-98de76e77d44", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEGy5SuUbf5AMi6M6HRUWoJ0tV7/hsDRIlBEXdb/iydbE7uezCAXcsNwdD5JpQJNt5g==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "ShopItem",
                keyColumn: "ShopItemID",
                keyValue: 4,
                column: "ShopID",
                value: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.UpdateData(
                table: "ShopItem",
                keyColumn: "ShopItemID",
                keyValue: 5,
                column: "ShopID",
                value: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.UpdateData(
                table: "ShopItem",
                keyColumn: "ShopItemID",
                keyValue: 6,
                column: "ShopID",
                value: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "937c303e-a51c-4906-9dff-f534bbc050b8", "89ff1b18-68a9-4072-971f-5c7a0d03b4d4" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItem_Shops_ShopID",
                table: "ShopItem",
                column: "ShopID",
                principalTable: "Shops",
                principalColumn: "ShopID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopItem_Shops_ShopID",
                table: "ShopItem");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "937c303e-a51c-4906-9dff-f534bbc050b8", "89ff1b18-68a9-4072-971f-5c7a0d03b4d4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "937c303e-a51c-4906-9dff-f534bbc050b8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89ff1b18-68a9-4072-971f-5c7a0d03b4d4");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShopID",
                table: "ShopItem",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34314760-5a81-471b-8f8b-0bddd278aa53", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4822405e-553a-4a39-b444-5f4e572cf489", 0, "576b7e76-a34f-4fb0-9b97-f1d3d426f340", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEKpHOrHavaxoBosTCr+wC3524UYu8BapQ/sP1/zDiemilglsEPiZSYu3fvRWtBzjZg==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "ShopItem",
                keyColumn: "ShopItemID",
                keyValue: 4,
                column: "ShopID",
                value: null);

            migrationBuilder.UpdateData(
                table: "ShopItem",
                keyColumn: "ShopItemID",
                keyValue: 5,
                column: "ShopID",
                value: null);

            migrationBuilder.UpdateData(
                table: "ShopItem",
                keyColumn: "ShopItemID",
                keyValue: 6,
                column: "ShopID",
                value: null);

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "34314760-5a81-471b-8f8b-0bddd278aa53", "4822405e-553a-4a39-b444-5f4e572cf489" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItem_Shops_ShopID",
                table: "ShopItem",
                column: "ShopID",
                principalTable: "Shops",
                principalColumn: "ShopID");
        }
    }
}
