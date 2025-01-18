using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace GameBookASP.GameModels;

public class PlayerItem
{
    [Key]
    public int PlayerItemId { get; set; }
    
    [ForeignKey("Player")]
    public Guid PlayerId { get; set; }
    
    [ForeignKey("Item")]
    public int ItemId { get; set; }
    
    public int Quantity { get; set; }

    [JsonIgnore]
    public Player Player { get; set; } = null!;
    
    [JsonIgnore]
    public Item Item { get; set; } = null!;
} 