using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GameBookASP.GameModels;

public class Player
{
    [Key]
    public required Guid PlayerID { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Health { get; set; }
    public int Withdrawal { get; set; }
    public int Stamina { get; set; }
    public int Coins { get; set; }
    public ICollection<Item> Inventory { get; set; } // on gwt posilat json
}
