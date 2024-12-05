using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Link
{
    [Key]
    public Guid LinkID { get; set; }
    [ForeignKey("FromLocation")]
    public Guid FromLocationID { get; set; }
    [ForeignKey("ToLocation")]
    public Guid ToLocationID { get; set; }
    public string? Condition { get; set; }

    public Location FromLocation { get; set; } = null!;
    public Location ToLocation { get; set; } = null!;
}
