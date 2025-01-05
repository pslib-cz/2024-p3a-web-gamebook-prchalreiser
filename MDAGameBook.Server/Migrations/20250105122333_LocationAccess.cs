using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class LocationAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "6605eb37-0f06-4646-b97e-4a62519374f5", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fdd1a8d8-6380-4d1f-a63e-4444e4042d07", 0, "3a87d3a3-4473-4040-85bc-fea990fe7e0a", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAECbvaYoXkzja2n7+Ps4zSNQdxlPGbTHa9Hr7NpDO0DCm1iLhhrarDtNF1b0BmpHkgA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "LinkID",
                keyValue: 1,
                column: "RequiredItemId",
                value: null);

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6605eb37-0f06-4646-b97e-4a62519374f5", "fdd1a8d8-6380-4d1f-a63e-4444e4042d07" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6605eb37-0f06-4646-b97e-4a62519374f5", "fdd1a8d8-6380-4d1f-a63e-4444e4042d07" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6605eb37-0f06-4646-b97e-4a62519374f5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fdd1a8d8-6380-4d1f-a63e-4444e4042d07");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "290cb8e8-c377-4464-a1d1-21e893426f62", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "af12501d-d11e-4d6d-a489-878c1c73f4c1", 0, "54c2f348-a6cd-4ba4-90e3-14b9ecbe8011", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEGugbbJrYAs/Dg0IeHGLmyWfbn84viFZ1ylctZLG8NjKRtFOG0/16HoaDGchW1cymw==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "LinkID",
                keyValue: 1,
                column: "RequiredItemId",
                value: 1);

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "290cb8e8-c377-4464-a1d1-21e893426f62", "af12501d-d11e-4d6d-a489-878c1c73f4c1" });
        }
    }
}
