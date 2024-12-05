using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class kokot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cb5ad632-e045-44a8-809b-2b584fd487db", "baea6a6d-6a6c-4977-914a-155619c8045c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb5ad632-e045-44a8-809b-2b584fd487db");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "baea6a6d-6a6c-4977-914a-155619c8045c");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImageUrl",
                table: "Locations",
                type: "TEXT",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "BackgroundImageUrl",
                table: "Locations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb5ad632-e045-44a8-809b-2b584fd487db", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "baea6a6d-6a6c-4977-914a-155619c8045c", 0, "ca938198-dde1-474e-b074-ec82f6d5e5cf", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEILOl8ZPTxzVAwKGMD5isziWBDMgEUJ7ye6PZkxir6plKjIQntbnTGddjBQF+M+EVA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cb5ad632-e045-44a8-809b-2b584fd487db", "baea6a6d-6a6c-4977-914a-155619c8045c" });
        }
    }
}
