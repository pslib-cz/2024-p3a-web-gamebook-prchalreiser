using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GameBookASP.GameModels;

public class Shop
{
    [Key]
    public Guid ShopID { get; set; }
    
    [ForeignKey("Location")]
    public int LocationID { get; set; }
    
    [Obsolete("Use ShopItems instead")]
    public string ItemsForSale { get; set; } = "[]";
    
    public List<ShopItem> ShopItems { get; set; } = new();
    
    [JsonIgnore]
    public Location Location { get; set; } = null!;
}

public class ShopItem
{
    [Key]
    public int ShopItemID { get; set; }
    public int ItemID { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    
    [ForeignKey("ItemID")]
    public Item Item { get; set; } = null!;

    [ForeignKey("Shop")]
    public Guid ShopID { get; set; }
    public Shop Shop { get; set; } = null!;
}
