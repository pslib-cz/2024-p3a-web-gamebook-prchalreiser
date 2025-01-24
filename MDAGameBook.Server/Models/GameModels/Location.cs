using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameBookASP.GameModels;
public class Location
{
    [Key]
    public int LocationID { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)] // Reasonable max length for descriptions
    public string? Description { get; set; }

    [StringLength(500)] // URL length limit
    public string? BackgroundImageUrl { get; set; }

    public bool HasRequiredItem { get; set; }
    public bool HasShop { get; set; }
    public bool HasMinigame { get; set; }

    // Remove the Items string property and replace with proper relationships
    public virtual ICollection<Item> AvailableItems { get; set; } = new List<Item>();
    
    // Add navigation properties for better relationship management
    public virtual ICollection<Link> OutgoingLinks { get; set; } = new List<Link>();
    public virtual ICollection<Link> IncomingLinks { get; set; } = new List<Link>();
    public virtual Shop? Shop { get; set; }
    public virtual ICollection<Minigame> Minigames { get; set; } = new List<Minigame>();
}
