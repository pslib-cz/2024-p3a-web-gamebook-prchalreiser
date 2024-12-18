using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class ItemID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "HasRequiredItem",
                table: "Locations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ItemID",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "391453fa-4ea0-4a38-8d45-78fab534c2e3", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5b41d506-e1fa-48f9-8430-aea4f4e23788", 0, "96f50483-1c1e-4d3a-9800-caefd37b46f3", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAECRjuAI+bB/I7P2N5T9I7bArviQvVp8cOEvVs/m33+FFIFU8rNOOs8/aA+hslvDotg==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemID", "Description", "Effect", "FightMinigameMinigameID", "IsDrinkable", "Name", "PlayerID", "Price" },
                values: new object[] { 1, "A mysterious key that unlocks the path forward.", "{}", null, false, "Magic Key", null, 0 });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: -1,
                column: "HasRequiredItem",
                value: false);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 420,
                column: "HasRequiredItem",
                value: true);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 421,
                column: "HasRequiredItem",
                value: false);

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerID", "Coins", "Health", "Name", "Stamina", "Withdrawal" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), 0, 100, "", 100, 0 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "391453fa-4ea0-4a38-8d45-78fab534c2e3", "5b41d506-e1fa-48f9-8430-aea4f4e23788" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "391453fa-4ea0-4a38-8d45-78fab534c2e3", "5b41d506-e1fa-48f9-8430-aea4f4e23788" });

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerID",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "391453fa-4ea0-4a38-8d45-78fab534c2e3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5b41d506-e1fa-48f9-8430-aea4f4e23788");

            migrationBuilder.DropColumn(
                name: "HasRequiredItem",
                table: "Locations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ItemID",
                table: "Items",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

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
    }
}
