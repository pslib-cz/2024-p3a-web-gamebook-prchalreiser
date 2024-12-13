using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedingMig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "UserPlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPlayers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "602e38eb-1c1d-4645-839e-5f92e6a5311c", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3ea7e5af-d276-4f22-b99b-7877c86a0ffb", 0, "5aa1f6eb-ef94-48d2-b5aa-189d8e5de036", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEF48gwDnhpizkDuYsFXE9vOsazsQ7TvY1YW3P3w/h61zL6HevgutKd2P1mWCipSyhA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: -1,
                column: "BackgroundImageUrl",
                value: "https://localhost:7260/Uploads/f5f2add3-d635-4319-8e27-d9494c03b14e.png");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 420,
                column: "BackgroundImageUrl",
                value: "https://localhost:7260/Uploads/06dfd75a-1c7b-42a2-942d-ee3d48a26a0f.png");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 421,
                column: "BackgroundImageUrl",
                value: "https://localhost:7260/Uploads/06dfd75a-1c7b-42a2-942d-ee3d48a26a0f.png");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "602e38eb-1c1d-4645-839e-5f92e6a5311c", "3ea7e5af-d276-4f22-b99b-7877c86a0ffb" });

            migrationBuilder.CreateIndex(
                name: "IX_UserPlayers_PlayerId",
                table: "UserPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlayers_UserId",
                table: "UserPlayers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPlayers");

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
                values: new object[] { "eb4362e0-d16c-4190-8df8-80d27004003f", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "71c63911-afe4-44f0-aaf6-579418d20cc1", 0, "bcada984-bfb8-404a-8824-ded18c07a702", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEN1pWuWSDvAa0vn42XpbfR8HGDKzoFQPTzhy5l6tyaU9slL2Ym1JoIimExl2l/ue3Q==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: -1,
                column: "BackgroundImageUrl",
                value: "https://localhost:7260/wwwroot/Uploads/06dfd75a-1c7b-42a2-942d-ee3d48a26a0f.png");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 420,
                column: "BackgroundImageUrl",
                value: "https://localhost:7260/wwwroot/Uploads/06dfd75a-1c7b-42a2-942d-ee3d48a26a0f.png");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 421,
                column: "BackgroundImageUrl",
                value: "https://localhost:7260/wwwroot/Uploads/06dfd75a-1c7b-42a2-942d-ee3d48a26a0f.png");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "eb4362e0-d16c-4190-8df8-80d27004003f", "71c63911-afe4-44f0-aaf6-579418d20cc1" });
        }
    }
}
