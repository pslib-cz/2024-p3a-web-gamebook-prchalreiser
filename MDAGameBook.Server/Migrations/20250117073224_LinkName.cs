using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class LinkName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f8abe114-e79a-4ed0-9092-76d8a241e403", "a684dbb1-d287-4cd9-b6d8-3516c0b1f6ad" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8abe114-e79a-4ed0-9092-76d8a241e403");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a684dbb1-d287-4cd9-b6d8-3516c0b1f6ad");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Links",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13a55dc5-3cbc-467c-8a07-20c7cdec357e", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "190e5b36-56d6-4a28-aa02-1eaf62d2f470", 0, "db988e12-83c4-448b-ba9d-51e3d1f04e37", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEPrioEG+d7kRJsd9kLGuyICIsydTV0SCfRok5HeMh6Y057Vxwn/H5Jv4YiHwUDRC/A==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "13a55dc5-3cbc-467c-8a07-20c7cdec357e", "190e5b36-56d6-4a28-aa02-1eaf62d2f470" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Links");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f8abe114-e79a-4ed0-9092-76d8a241e403", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a684dbb1-d287-4cd9-b6d8-3516c0b1f6ad", 0, "b8e43cc1-aa41-42f1-8476-c392d8191113", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEIc0idWkVx/WYPvwOMLiO3Yh7K7RHHVLJoe6hgi1Zdum+Vr1v7Qo8j/7Xb9MM9pugQ==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f8abe114-e79a-4ed0-9092-76d8a241e403", "a684dbb1-d287-4cd9-b6d8-3516c0b1f6ad" });
        }
    }
}
