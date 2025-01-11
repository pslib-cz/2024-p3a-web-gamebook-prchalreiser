using Microsoft.EntityFrameworkCore.Migrations;

public partial class UpdateImageUrls : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Update existing URLs to remove the host/port
        migrationBuilder.Sql(@"
            UPDATE Locations 
            SET BackgroundImageUrl = REPLACE(BackgroundImageUrl, 'https://localhost:7260/', '/')
            WHERE BackgroundImageUrl LIKE 'https://localhost:7260/%'
        ");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"
            UPDATE Locations 
            SET BackgroundImageUrl = REPLACE(BackgroundImageUrl, '/', 'https://localhost:7260/')
            WHERE BackgroundImageUrl LIKE '/%'
        ");
    }
}