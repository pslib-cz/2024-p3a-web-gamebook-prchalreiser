using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GameBookASP.GameModels;

public class Minigame
{
    [Key]
    public Guid MinigameID { get; set; }
    [ForeignKey("Location")]
    public int LocationID { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // To distinguish between different minigame types
    public bool IsCompleted { get; set; } = false;
    public int PlayerScore { get; set; } = 0;
    public int ComputerScore { get; set; } = 0;

    public Location Location { get; set; } = null!;
}
