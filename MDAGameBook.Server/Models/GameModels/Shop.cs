using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GameBookASP.GameModels;

public class Shop
{
    [Key]
    public Guid ShopID { get; set; }
    [ForeignKey("Location")]
    public int LocationID { get; set; }
    public string ItemsForSale { get; set; } = "[]"; // JSON representation of items

    public Location Location { get; set; } = null!;
}
