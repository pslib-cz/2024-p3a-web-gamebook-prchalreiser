using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class RequiredItemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "391453fa-4ea0-4a38-8d45-78fab534c2e3", "5b41d506-e1fa-48f9-8430-aea4f4e23788" });

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "LinkID",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "391453fa-4ea0-4a38-8d45-78fab534c2e3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5b41d506-e1fa-48f9-8430-aea4f4e23788");

            migrationBuilder.AddColumn<int>(
                name: "RequiredItemId",
                table: "Links",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cff77e05-b263-4eae-8704-3fe9925290af", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "37acea0d-71d3-4365-8a98-f982bea1ecbd", 0, "209c443a-be48-4c03-a8cd-b1a239fbcbf2", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEMKLhdB7iNyxisKp+UkDVvVDPUWQh8ZyWR3PCYUeZ21dUss4bbgTVOb3UoQvwN0qdQ==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "LinkID", "Condition", "FromLocationID", "RequiredItemId", "ToLocationID" },
                values: new object[] { 1, null, 420, 1, 421 });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 421,
                columns: new[] { "BackgroundImageUrl", "Description", "Name" },
                values: new object[] { "https://localhost:7260/Uploads/dark-forest.png", "Vylezl jsi z auta a stojíš v temném lese. Cesta se rozděluje na několik směrů.", "Dark Forest" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "BackgroundImageUrl", "Description", "HasRequiredItem", "Items", "Name" },
                values: new object[,]
                {
                    { 422, "https://localhost:7260/Uploads/cabin.png", "Narazil jsi na starou dřevěnou chatu. Vypadá opuštěně, ale světlo uvnitř stále svítí.", false, "[]", "Abandoned Cabin" },
                    { 423, "https://localhost:7260/Uploads/lake.png", "Přišel jsi k temnému jezeru. Měsíční světlo se odráží na jeho hladině.", false, "[]", "Mysterious Lake" },
                    { 424, "https://localhost:7260/Uploads/stone-circle.png", "Objevil jsi kruh prastarých kamenů. Ve vzduchu je cítit magická energie.", false, "[]", "Ancient Stone Circle" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cff77e05-b263-4eae-8704-3fe9925290af", "37acea0d-71d3-4365-8a98-f982bea1ecbd" });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "LinkID", "Condition", "FromLocationID", "RequiredItemId", "ToLocationID" },
                values: new object[,]
                {
                    { 2, null, 421, null, 422 },
                    { 3, null, 421, null, 423 },
                    { 4, null, 421, null, 424 },
                    { 5, null, 422, null, 421 },
                    { 6, null, 423, null, 421 },
                    { 7, null, 424, null, 421 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cff77e05-b263-4eae-8704-3fe9925290af", "37acea0d-71d3-4365-8a98-f982bea1ecbd" });

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "LinkID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "LinkID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "LinkID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "LinkID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "LinkID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "LinkID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "LinkID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cff77e05-b263-4eae-8704-3fe9925290af");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37acea0d-71d3-4365-8a98-f982bea1ecbd");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 422);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 423);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 424);

            migrationBuilder.DropColumn(
                name: "RequiredItemId",
                table: "Links");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "391453fa-4ea0-4a38-8d45-78fab534c2e3", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5b41d506-e1fa-48f9-8430-aea4f4e23788", 0, "96f50483-1c1e-4d3a-9800-caefd37b46f3", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAECRjuAI+bB/I7P2N5T9I7bArviQvVp8cOEvVs/m33+FFIFU8rNOOs8/aA+hslvDotg==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "LinkID", "Condition", "FromLocationID", "ToLocationID" },
                values: new object[] { 69, null, 420, 421 });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 421,
                columns: new[] { "BackgroundImageUrl", "Description", "Name" },
                values: new object[] { "https://localhost:7260/Uploads/06dfd75a-1c7b-42a2-942d-ee3d48a26a0f.png", "Vylezl jsi z auta a stojíš v temném lese", "Outside" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "391453fa-4ea0-4a38-8d45-78fab534c2e3", "5b41d506-e1fa-48f9-8430-aea4f4e23788" });
        }
    }
}
