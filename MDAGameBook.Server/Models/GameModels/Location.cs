using System.ComponentModel.DataAnnotations;

public class Location
{
    [Key]
    public Guid LocationID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Items { get; set; } = "[]"; // JSON representation of items
}
