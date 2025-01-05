using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class prdik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0bfcdbd8-29d2-4aec-9b88-0f52020e57f4", "de51bef5-c143-4c59-9192-32a93d99a38f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bfcdbd8-29d2-4aec-9b88-0f52020e57f4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "de51bef5-c143-4c59-9192-32a93d99a38f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "290cb8e8-c377-4464-a1d1-21e893426f62", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "af12501d-d11e-4d6d-a489-878c1c73f4c1", 0, "54c2f348-a6cd-4ba4-90e3-14b9ecbe8011", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEGugbbJrYAs/Dg0IeHGLmyWfbn84viFZ1ylctZLG8NjKRtFOG0/16HoaDGchW1cymw==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "290cb8e8-c377-4464-a1d1-21e893426f62", "af12501d-d11e-4d6d-a489-878c1c73f4c1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "290cb8e8-c377-4464-a1d1-21e893426f62", "af12501d-d11e-4d6d-a489-878c1c73f4c1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "290cb8e8-c377-4464-a1d1-21e893426f62");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "af12501d-d11e-4d6d-a489-878c1c73f4c1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0bfcdbd8-29d2-4aec-9b88-0f52020e57f4", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "de51bef5-c143-4c59-9192-32a93d99a38f", 0, "3075410c-b543-4822-9bfe-ed76ee0682a3", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEAeXW7YFqktrlqnb9jMxq2l186Bn2bMW1akAetxThFycqnBS9+AtcyINgE8WXSHBTA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0bfcdbd8-29d2-4aec-9b88-0f52020e57f4", "de51bef5-c143-4c59-9192-32a93d99a38f" });
        }
    }
}
