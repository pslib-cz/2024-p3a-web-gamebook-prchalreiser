using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class LinkPostErroring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "2ca3f63a-6a74-4beb-a216-49a15ab6467e", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f5f339b1-766c-41e6-91c5-e989bb2e66f8", 0, "639d5e8b-0776-40dc-9550-2698ce24303d", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEHIvt1gl/574AjmXBm5t/50R0BjD1/Fh/Leq3G4cMUniCENdhlPLHVbgxYQgdmry7Q==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2ca3f63a-6a74-4beb-a216-49a15ab6467e", "f5f339b1-766c-41e6-91c5-e989bb2e66f8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2ca3f63a-6a74-4beb-a216-49a15ab6467e", "f5f339b1-766c-41e6-91c5-e989bb2e66f8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ca3f63a-6a74-4beb-a216-49a15ab6467e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5f339b1-766c-41e6-91c5-e989bb2e66f8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "50ccf4ea-1c40-456b-81a1-e958d0e7dfc2", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "da71bb61-65fd-4758-ab0b-d85314217dbf", 0, "833d85c1-9359-4fb3-8fdc-7611ab20303a", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEGZywXvlM2jRgAbvsCNaEm0D3sX9K8hNRoaEzB9A4clSAqFV19hnMNyNhtNn0e8htw==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "50ccf4ea-1c40-456b-81a1-e958d0e7dfc2", "da71bb61-65fd-4758-ab0b-d85314217dbf" });
        }
    }
}
