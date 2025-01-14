using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class cosetadypresnedeje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d3c82f15-2d96-4b13-b75a-37a3a57bdaae", "61af0114-3739-42f8-a842-c138b9e670e5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3c82f15-2d96-4b13-b75a-37a3a57bdaae");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61af0114-3739-42f8-a842-c138b9e670e5");

            migrationBuilder.AddColumn<bool>(
                name: "HasMinigame",
                table: "Locations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f8abe114-e79a-4ed0-9092-76d8a241e403", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a684dbb1-d287-4cd9-b6d8-3516c0b1f6ad", 0, "b8e43cc1-aa41-42f1-8476-c392d8191113", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEIc0idWkVx/WYPvwOMLiO3Yh7K7RHHVLJoe6hgi1Zdum+Vr1v7Qo8j/7Xb9MM9pugQ==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 55,
                column: "HasMinigame",
                value: false);

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f8abe114-e79a-4ed0-9092-76d8a241e403", "a684dbb1-d287-4cd9-b6d8-3516c0b1f6ad" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "HasMinigame",
                table: "Locations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3c82f15-2d96-4b13-b75a-37a3a57bdaae", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "61af0114-3739-42f8-a842-c138b9e670e5", 0, "6ab74945-232d-42d5-ad88-7e49cbcb4239", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEDsNhXr1LQEfwU+wui/ckw0uQaPHRBJBhHXPdNRcC6SZygcdrCJYdSKfkssvCd19Bw==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d3c82f15-2d96-4b13-b75a-37a3a57bdaae", "61af0114-3739-42f8-a842-c138b9e670e5" });
        }
    }
}
