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
    public string OpponentName { get; set; } = "Computer";
    public int WinLocationID { get; set; }
    public int LoseLocationID { get; set; }
    public string? Number1 { get; set; }
    public string? Number2 { get; set; }

    public Location Location { get; set; } = null!;
    public ICollection<PlayerMinigame> PlayerMinigames { get; set; } = new List<PlayerMinigame>();
}
