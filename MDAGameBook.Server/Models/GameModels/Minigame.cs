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
    public string Type { get; set; } = string.Empty;

    // Remove these properties as they'll be moved to PlayerMinigame
    [NotMapped]
    public bool IsCompleted { get; set; } = false;
    [NotMapped]
    public int PlayerScore { get; set; } = 0;
    [NotMapped]
    public int ComputerScore { get; set; } = 0;


    public Location Location { get; set; } = null!;
    public ICollection<PlayerMinigame> PlayerMinigames { get; set; } = new List<PlayerMinigame>();
}
