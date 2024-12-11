using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Minigame
{
    [Key]
    public Guid MinigameID { get; set; }
    [ForeignKey("Location")]
    public int LocationID { get; set; }
    public string Description { get; set; } = string.Empty;

    public Location Location { get; set; } = null!;
}
