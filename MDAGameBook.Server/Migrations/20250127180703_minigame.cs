using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class minigame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "110b92d3-1b9c-4e98-8364-0a84a9891791", "b8847ae2-1fa7-44ae-9949-712b67599b78" });

            migrationBuilder.DeleteData(
                table: "Minigames",
                keyColumn: "MinigameID",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "110b92d3-1b9c-4e98-8364-0a84a9891791");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b8847ae2-1fa7-44ae-9949-712b67599b78");

            migrationBuilder.AddColumn<int>(
                name: "LoseLocationID",
                table: "Minigames",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OpponentName",
                table: "Minigames",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WinLocationID",
                table: "Minigames",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "53d8ea19-c63d-49bd-85af-6c670fb81cf2", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fcbcd934-260a-4138-8474-5e215c351a64", 0, "d62e3693-1625-47c6-829e-e23f60b9893f", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEMgiDeLo9oaRSz8TVMApA5SSEbjbbl4rAw95BkcMFJeb5wn2/h61nriU+cw01OZtOw==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Minigames",
                columns: new[] { "MinigameID", "Description", "LocationID", "LoseLocationID", "OpponentName", "Type", "WinLocationID" },
                values: new object[] { new Guid("550e8400-e29b-41d4-a716-446655440000"), "Rock Paper Scissors Challenge", 1, 3, "Mysterious Stranger", "RPS", 2 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "53d8ea19-c63d-49bd-85af-6c670fb81cf2", "fcbcd934-260a-4138-8474-5e215c351a64" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "53d8ea19-c63d-49bd-85af-6c670fb81cf2", "fcbcd934-260a-4138-8474-5e215c351a64" });

            migrationBuilder.DeleteData(
                table: "Minigames",
                keyColumn: "MinigameID",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53d8ea19-c63d-49bd-85af-6c670fb81cf2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fcbcd934-260a-4138-8474-5e215c351a64");

            migrationBuilder.DropColumn(
                name: "LoseLocationID",
                table: "Minigames");

            migrationBuilder.DropColumn(
                name: "OpponentName",
                table: "Minigames");

            migrationBuilder.DropColumn(
                name: "WinLocationID",
                table: "Minigames");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "110b92d3-1b9c-4e98-8364-0a84a9891791", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b8847ae2-1fa7-44ae-9949-712b67599b78", 0, "e35b9abf-a98e-4d23-8fdc-d861aacc825d", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEBhSLzsoeh1k7/SgdH+IlI4Ry8zuuGyFHF0jCfNNbAae2goaXLjnYrbstZj/q7VyaA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Minigames",
                columns: new[] { "MinigameID", "Description", "LocationID", "Type" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), "Challenge the computer to a game of Rock Paper Scissors! First to 3 wins.", 55, "RPS" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "110b92d3-1b9c-4e98-8364-0a84a9891791", "b8847ae2-1fa7-44ae-9949-712b67599b78" });
        }
    }
}
