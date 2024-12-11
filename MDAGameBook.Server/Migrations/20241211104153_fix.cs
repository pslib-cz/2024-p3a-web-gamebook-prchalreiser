using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Locations_LocationReachedID",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_LocationReachedID",
                table: "Players");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "427c0af7-a4a5-454a-b713-2701f13769d1", "b445fdb3-8394-4cfd-b987-7828ec249fb2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "427c0af7-a4a5-454a-b713-2701f13769d1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b445fdb3-8394-4cfd-b987-7828ec249fb2");

            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LocationReachedID",
                table: "Players");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerID",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0d5f29c5-bd95-4e97-ac99-56e49869c556", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "93107250-e569-4830-a4b1-2e409e8af533", 0, "dd22e8fa-ea6d-4498-b1e9-4d4f497df4c2", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEBOmCS8b2+M8QE6MM9Wu9Gdn4vJNcrEimZ5YMiLBtJooPNgIM4C8/Q1pGmUf7XuvLw==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0d5f29c5-bd95-4e97-ac99-56e49869c556", "93107250-e569-4830-a4b1-2e409e8af533" });

            migrationBuilder.CreateIndex(
                name: "IX_Items_PlayerID",
                table: "Items",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Players_PlayerID",
                table: "Items",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "PlayerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Players_PlayerID",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_PlayerID",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0d5f29c5-bd95-4e97-ac99-56e49869c556", "93107250-e569-4830-a4b1-2e409e8af533" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d5f29c5-bd95-4e97-ac99-56e49869c556");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93107250-e569-4830-a4b1-2e409e8af533");

            migrationBuilder.DropColumn(
                name: "PlayerID",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Inventory",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LocationReachedID",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "427c0af7-a4a5-454a-b713-2701f13769d1", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b445fdb3-8394-4cfd-b987-7828ec249fb2", 0, "3116150d-b41a-4cc4-86db-f6c4b86b1a85", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEFnEndHyNGpE9RT7pn6RXdFZ1bLSTUm7BYtF4wzkEr4H9Kjf7wasUqwL9RWvfY7YfA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "427c0af7-a4a5-454a-b713-2701f13769d1", "b445fdb3-8394-4cfd-b987-7828ec249fb2" });

            migrationBuilder.CreateIndex(
                name: "IX_Players_LocationReachedID",
                table: "Players",
                column: "LocationReachedID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Locations_LocationReachedID",
                table: "Players",
                column: "LocationReachedID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
