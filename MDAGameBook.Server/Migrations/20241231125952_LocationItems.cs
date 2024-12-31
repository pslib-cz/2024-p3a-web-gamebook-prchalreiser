using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class LocationItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cff77e05-b263-4eae-8704-3fe9925290af", "37acea0d-71d3-4365-8a98-f982bea1ecbd" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cff77e05-b263-4eae-8704-3fe9925290af");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37acea0d-71d3-4365-8a98-f982bea1ecbd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "50ccf4ea-1c40-456b-81a1-e958d0e7dfc2", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "da71bb61-65fd-4758-ab0b-d85314217dbf", 0, "833d85c1-9359-4fb3-8fdc-7611ab20303a", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEGZywXvlM2jRgAbvsCNaEm0D3sX9K8hNRoaEzB9A4clSAqFV19hnMNyNhtNn0e8htw==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 420,
                column: "Items",
                value: "[1]");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "50ccf4ea-1c40-456b-81a1-e958d0e7dfc2", "da71bb61-65fd-4758-ab0b-d85314217dbf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "50ccf4ea-1c40-456b-81a1-e958d0e7dfc2", "da71bb61-65fd-4758-ab0b-d85314217dbf" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50ccf4ea-1c40-456b-81a1-e958d0e7dfc2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da71bb61-65fd-4758-ab0b-d85314217dbf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cff77e05-b263-4eae-8704-3fe9925290af", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "37acea0d-71d3-4365-8a98-f982bea1ecbd", 0, "209c443a-be48-4c03-a8cd-b1a239fbcbf2", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEMKLhdB7iNyxisKp+UkDVvVDPUWQh8ZyWR3PCYUeZ21dUss4bbgTVOb3UoQvwN0qdQ==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 420,
                column: "Items",
                value: "[]");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cff77e05-b263-4eae-8704-3fe9925290af", "37acea0d-71d3-4365-8a98-f982bea1ecbd" });
        }
    }
}
