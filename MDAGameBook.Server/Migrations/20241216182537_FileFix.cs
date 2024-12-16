using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class FileFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "602e38eb-1c1d-4645-839e-5f92e6a5311c", "3ea7e5af-d276-4f22-b99b-7877c86a0ffb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "602e38eb-1c1d-4645-839e-5f92e6a5311c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3ea7e5af-d276-4f22-b99b-7877c86a0ffb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "783c81f8-9cb3-4d0c-a1c1-2a9d4590462b", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6c7ea611-d23a-41a4-9352-f30b369a0923", 0, "e43da463-aa34-44eb-bf52-410f61a21d61", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEJnNuv5xG/48Orfe/4fTwQoAmVjmcJ5dBxNyq+VDK1PNo1yDfRf5PQBPQ4DTg1NRKQ==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "783c81f8-9cb3-4d0c-a1c1-2a9d4590462b", "6c7ea611-d23a-41a4-9352-f30b369a0923" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "783c81f8-9cb3-4d0c-a1c1-2a9d4590462b", "6c7ea611-d23a-41a4-9352-f30b369a0923" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "783c81f8-9cb3-4d0c-a1c1-2a9d4590462b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6c7ea611-d23a-41a4-9352-f30b369a0923");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "602e38eb-1c1d-4645-839e-5f92e6a5311c", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3ea7e5af-d276-4f22-b99b-7877c86a0ffb", 0, "5aa1f6eb-ef94-48d2-b5aa-189d8e5de036", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEF48gwDnhpizkDuYsFXE9vOsazsQ7TvY1YW3P3w/h61zL6HevgutKd2P1mWCipSyhA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "602e38eb-1c1d-4645-839e-5f92e6a5311c", "3ea7e5af-d276-4f22-b99b-7877c86a0ffb" });
        }
    }
}
