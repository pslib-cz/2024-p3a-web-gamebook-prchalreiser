using System.ComponentModel.DataAnnotations;
namespace GameBookASP.GameModels;
public class Location
{
    [Key]
    public int LocationID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Items { get; set; } = "[]"; // JSON representation of items)
    public string? BackgroundImageUrl { get; set; }

}
