using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class uznepls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "53d8ea19-c63d-49bd-85af-6c670fb81cf2", "fcbcd934-260a-4138-8474-5e215c351a64" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53d8ea19-c63d-49bd-85af-6c670fb81cf2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fcbcd934-260a-4138-8474-5e215c351a64");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "80462a9b-d956-498f-8273-92d1cd8b2b6b", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2d4ce928-d755-4965-9487-64dcdc75beb4", 0, "4f9d9387-4746-480a-b6f2-778ddee0e648", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEOESIwbAAguuKPMLbedb2LgCGil7HrqCujU5cLLnoVIGoHqgUxdFQXGo0cH9do0gKA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 55,
                column: "HasMinigame",
                value: true);

            migrationBuilder.UpdateData(
                table: "Minigames",
                keyColumn: "MinigameID",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"),
                columns: new[] { "Description", "LocationID", "LoseLocationID", "WinLocationID" },
                values: new object[] { "Challenge the mysterious stranger to a game of Rock Paper Scissors! First to 3 wins.", 55, 57, 56 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "80462a9b-d956-498f-8273-92d1cd8b2b6b", "2d4ce928-d755-4965-9487-64dcdc75beb4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "80462a9b-d956-498f-8273-92d1cd8b2b6b", "2d4ce928-d755-4965-9487-64dcdc75beb4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80462a9b-d956-498f-8273-92d1cd8b2b6b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d4ce928-d755-4965-9487-64dcdc75beb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "53d8ea19-c63d-49bd-85af-6c670fb81cf2", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fcbcd934-260a-4138-8474-5e215c351a64", 0, "d62e3693-1625-47c6-829e-e23f60b9893f", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEMgiDeLo9oaRSz8TVMApA5SSEbjbbl4rAw95BkcMFJeb5wn2/h61nriU+cw01OZtOw==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 55,
                column: "HasMinigame",
                value: false);

            migrationBuilder.UpdateData(
                table: "Minigames",
                keyColumn: "MinigameID",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"),
                columns: new[] { "Description", "LocationID", "LoseLocationID", "WinLocationID" },
                values: new object[] { "Rock Paper Scissors Challenge", 1, 3, 2 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "53d8ea19-c63d-49bd-85af-6c670fb81cf2", "fcbcd934-260a-4138-8474-5e215c351a64" });
        }
    }
}
