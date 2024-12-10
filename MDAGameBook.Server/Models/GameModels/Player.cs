using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Player
{
    [Key]
    public required Guid PlayerID { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Health { get; set; }
    public int Withdrawal { get; set; }
    public int Stamina { get; set; }
    public int Coins { get; set; }
    public string Inventory { get; set; } = "[]"; // JSON representation of items

    [ForeignKey("LocationReached")]
    public int LocationReachedID { get; set; } = 0;
    public Location? LocationReached { get; set; }
}
