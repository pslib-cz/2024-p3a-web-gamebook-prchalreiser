using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class mda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "871ea4e7-6eab-4211-b943-fa43af10314c", "4e71aca7-c5a4-429e-87f9-2b63e2b980a5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "871ea4e7-6eab-4211-b943-fa43af10314c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e71aca7-c5a4-429e-87f9-2b63e2b980a5");

            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Shops",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "LocationReachedID",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Minigames",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Locations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "ToLocationID",
                table: "Links",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "FromLocationID",
                table: "Links",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<bool>(
                name: "Condition",
                table: "Links",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LinkID",
                table: "Links",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "LocationReachedID",
                table: "Players");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationID",
                table: "Shops",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationID",
                table: "Minigames",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationID",
                table: "Locations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ToLocationID",
                table: "Links",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "FromLocationID",
                table: "Links",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Condition",
                table: "Links",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LinkID",
                table: "Links",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "871ea4e7-6eab-4211-b943-fa43af10314c", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4e71aca7-c5a4-429e-87f9-2b63e2b980a5", 0, "66a76845-4003-4648-bc1f-0efa92d9f7b5", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEGAbRrcRQCPWPoLeVi+eVPd25eiZToa+0yUo9wXl162YImJkKyx3rawZ3WHZ5MyCuA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "871ea4e7-6eab-4211-b943-fa43af10314c", "4e71aca7-c5a4-429e-87f9-2b63e2b980a5" });
        }
    }
}
