using System.ComponentModel.DataAnnotations;

public class Player
{
    [Key]
    public Guid PlayerID { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Health { get; set; }
    public int Withdrawal { get; set; }
    public int Stamina { get; set; }
    public int Coins { get; set; }
    public string Inventory { get; set; } = "[]"; // JSON representation of items
}
