using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class smrdiprdi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "0718c044-bd98-4abd-ae94-12ac62941479", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "31ba445d-daaf-4fff-bd7e-32aba16ee02b", 0, "fb5a51bb-25c6-45b7-88c4-228fc2f2975f", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEJX4rVHZbDpNtBJtSugxOdJVKMu+TdwtI3U/c/wwFI8LdpG7ov/5v7gMZa3H5wsC6A==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0718c044-bd98-4abd-ae94-12ac62941479", "31ba445d-daaf-4fff-bd7e-32aba16ee02b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0718c044-bd98-4abd-ae94-12ac62941479", "31ba445d-daaf-4fff-bd7e-32aba16ee02b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0718c044-bd98-4abd-ae94-12ac62941479");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "31ba445d-daaf-4fff-bd7e-32aba16ee02b");

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
    }
}
