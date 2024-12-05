using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class FightMinigame
{
    [Key]
    [ForeignKey("Minigame")]
    public Guid MinigameID { get; set; }
    public string EnemyName { get; set; } = string.Empty;
    public int EnemyHealth { get; set; }
    public int EnemyStrength { get; set; }
    public string? VictoryReward { get; set; } = "[]"; // JSON representation of items
    public string StatPenalty { get; set; } = "{}"; // JSON representation of stat penalties

    public Minigame Minigame { get; set; } = null!;
}
