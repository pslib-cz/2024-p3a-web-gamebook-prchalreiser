using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class LinkLocationSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Locations_FromLocationID",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Locations_ToLocationID",
                table: "Links");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6fc16aac-3572-43cd-83f3-d8720a30b3a3", "1eaba987-5223-4c46-bfb9-6eaa27d34f12" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fc16aac-3572-43cd-83f3-d8720a30b3a3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1eaba987-5223-4c46-bfb9-6eaa27d34f12");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eb4362e0-d16c-4190-8df8-80d27004003f", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "71c63911-afe4-44f0-aaf6-579418d20cc1", 0, "bcada984-bfb8-404a-8824-ded18c07a702", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEN1pWuWSDvAa0vn42XpbfR8HGDKzoFQPTzhy5l6tyaU9slL2Ym1JoIimExl2l/ue3Q==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "eb4362e0-d16c-4190-8df8-80d27004003f", "71c63911-afe4-44f0-aaf6-579418d20cc1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Locations_FromLocationID",
                table: "Links",
                column: "FromLocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Locations_ToLocationID",
                table: "Links",
                column: "ToLocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Locations_FromLocationID",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Locations_ToLocationID",
                table: "Links");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eb4362e0-d16c-4190-8df8-80d27004003f", "71c63911-afe4-44f0-aaf6-579418d20cc1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb4362e0-d16c-4190-8df8-80d27004003f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71c63911-afe4-44f0-aaf6-579418d20cc1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6fc16aac-3572-43cd-83f3-d8720a30b3a3", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1eaba987-5223-4c46-bfb9-6eaa27d34f12", 0, "e2abe5e0-fa72-49b7-8d37-90aa70836eea", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEHzZ0bFu62kDMxMwhphnJrRkNJwRQCxG5/bBJtRyyM15mfq3Fja7DnxO1SzNtolIzQ==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6fc16aac-3572-43cd-83f3-d8720a30b3a3", "1eaba987-5223-4c46-bfb9-6eaa27d34f12" });

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Locations_FromLocationID",
                table: "Links",
                column: "FromLocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Locations_ToLocationID",
                table: "Links",
                column: "ToLocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
