using System.ComponentModel.DataAnnotations;
namespace GameBookASP.GameModels;

public class Item
{
    [Key]
    public Guid ItemID { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDrinkable { get; set; }
    public string? Description { get; set; }
    public int? Price { get; set; }
    public string Effect { get; set; } = "{}"; // JSON representation of effects
}