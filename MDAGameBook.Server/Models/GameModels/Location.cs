using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameBookASP.GameModels;
public class Location
{
    [Key]
    public int LocationID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    private string _items = "[]";
    public string Items
    {
        get => _items;
        set => _items = string.IsNullOrEmpty(value) ? "[]" : value;
    }
    public string? BackgroundImageUrl { get; set; }
    public bool HasRequiredItem { get; set; } = false;
    public bool HasShop { get; set; } = false;
    [JsonIgnore]
    public Shop? Shop { get; set; }

    public void SetItems(int[] items)
    {
        _items = JsonSerializer.Serialize(items);
    }

    public int[] GetItems()
    {
        return JsonSerializer.Deserialize<int[]>(_items) ?? Array.Empty<int>();
    }
}
