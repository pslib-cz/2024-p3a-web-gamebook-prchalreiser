using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class LastSavedLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "LastLocationID",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0bfcdbd8-29d2-4aec-9b88-0f52020e57f4", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "de51bef5-c143-4c59-9192-32a93d99a38f", 0, "3075410c-b543-4822-9bfe-ed76ee0682a3", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEAeXW7YFqktrlqnb9jMxq2l186Bn2bMW1akAetxThFycqnBS9+AtcyINgE8WXSHBTA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerID",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "LastLocationID",
                value: -1);

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0bfcdbd8-29d2-4aec-9b88-0f52020e57f4", "de51bef5-c143-4c59-9192-32a93d99a38f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0bfcdbd8-29d2-4aec-9b88-0f52020e57f4", "de51bef5-c143-4c59-9192-32a93d99a38f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bfcdbd8-29d2-4aec-9b88-0f52020e57f4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "de51bef5-c143-4c59-9192-32a93d99a38f");

            migrationBuilder.DropColumn(
                name: "LastLocationID",
                table: "Players");

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
    }
}
