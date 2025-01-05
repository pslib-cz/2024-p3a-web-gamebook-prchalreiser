using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MDAGameBook.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLocs : Migration
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
                keyValues: new object[] { "6605eb37-0f06-4646-b97e-4a62519374f5", "fdd1a8d8-6380-4d1f-a63e-4444e4042d07" });

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
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6605eb37-0f06-4646-b97e-4a62519374f5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fdd1a8d8-6380-4d1f-a63e-4444e4042d07");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationID",
                keyValue: 421);

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1b2dfed5-cb5f-4d3a-963f-6178641cfa50", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "be73ab8d-bbea-4f1d-bd92-74035825c34d", 0, "e01e1d13-7def-4059-90b1-c35668b15552", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAEBUjVQBT279fmsavqjt0OG9xgP/NF3DFpMWtccZklObdD7yDm2JkQxT4c+y0A/EU8g==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1b2dfed5-cb5f-4d3a-963f-6178641cfa50", "be73ab8d-bbea-4f1d-bd92-74035825c34d" });

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
                keyValues: new object[] { "1b2dfed5-cb5f-4d3a-963f-6178641cfa50", "be73ab8d-bbea-4f1d-bd92-74035825c34d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b2dfed5-cb5f-4d3a-963f-6178641cfa50");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "be73ab8d-bbea-4f1d-bd92-74035825c34d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6605eb37-0f06-4646-b97e-4a62519374f5", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fdd1a8d8-6380-4d1f-a63e-4444e4042d07", 0, "3a87d3a3-4473-4040-85bc-fea990fe7e0a", "admin@minjiya.com", true, false, null, "ADMIN@MINJIYA.COM", "ADMIN@MINJIYA.COM", "AQAAAAIAAYagAAAAECbvaYoXkzja2n7+Ps4zSNQdxlPGbTHa9Hr7NpDO0DCm1iLhhrarDtNF1b0BmpHkgA==", null, false, "", false, "admin@minjiya.com" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "BackgroundImageUrl", "Description", "HasRequiredItem", "Items", "Name" },
                values: new object[,]
                {
                    { -1, "https://localhost:7260/Uploads/f5f2add3-d635-4319-8e27-d9494c03b14e.png", "Nacházíš se někde, kam ses neměl dostat :O", false, "[]", "Unlinked Location" },
                    { 420, "https://localhost:7260/Uploads/06dfd75a-1c7b-42a2-942d-ee3d48a26a0f.png", "Jsi v interiéru auta a dáváš hotbox.", true, "[1]", "Hotbox" },
                    { 421, "https://localhost:7260/Uploads/dark-forest.png", "Vylezl jsi z auta a stojíš v temném lese. Cesta se rozděluje na několik směrů.", false, "[]", "Dark Forest" },
                    { 422, "https://localhost:7260/Uploads/cabin.png", "Narazil jsi na starou dřevěnou chatu. Vypadá opuštěně, ale světlo uvnitř stále svítí.", false, "[]", "Abandoned Cabin" },
                    { 423, "https://localhost:7260/Uploads/lake.png", "Přišel jsi k temnému jezeru. Měsíční světlo se odráží na jeho hladině.", false, "[]", "Mysterious Lake" },
                    { 424, "https://localhost:7260/Uploads/stone-circle.png", "Objevil jsi kruh prastarých kamenů. Ve vzduchu je cítit magická energie.", false, "[]", "Ancient Stone Circle" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6605eb37-0f06-4646-b97e-4a62519374f5", "fdd1a8d8-6380-4d1f-a63e-4444e4042d07" });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "LinkID", "Condition", "FromLocationID", "RequiredItemId", "ToLocationID" },
                values: new object[,]
                {
                    { 1, null, 420, null, 421 },
                    { 2, null, 421, null, 422 },
                    { 3, null, 421, null, 423 },
                    { 4, null, 421, null, 424 },
                    { 5, null, 422, null, 421 },
                    { 6, null, 423, null, 421 },
                    { 7, null, 424, null, 421 }
                });

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
    }
}
