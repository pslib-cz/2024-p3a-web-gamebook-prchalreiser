using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBookASP.GameModels;

public class PlayerMinigame
{
    [Key]
    public Guid PlayerMinigameID { get; set; }

    [ForeignKey("Player")]
    public Guid PlayerID { get; set; }

    [ForeignKey("Minigame")]
    public Guid MinigameID { get; set; }

    public bool IsCompleted { get; set; } = false;
    public int PlayerScore { get; set; } = 0;
    public int ComputerScore { get; set; } = 0;

    public Player Player { get; set; } = null!;
    public Minigame Minigame { get; set; } = null!;
} 