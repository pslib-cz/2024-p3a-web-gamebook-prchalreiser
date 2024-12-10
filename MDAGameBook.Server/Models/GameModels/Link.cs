using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Link
{
    [Key]
    public int LinkID { get; set; }
    [ForeignKey("FromLocation")]
    public int FromLocationID { get; set; }
    [ForeignKey("ToLocation")]
    public int ToLocationID { get; set; }
    public bool? Condition { get; set; }

    public Location FromLocation { get; set; } = null!;
    public Location ToLocation { get; set; } = null!;
}
