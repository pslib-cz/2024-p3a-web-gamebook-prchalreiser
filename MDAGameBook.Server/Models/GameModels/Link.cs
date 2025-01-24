using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace GameBookASP.GameModels;

public class Link
{
    [Key]
    public int LinkID { get; set; }
    [Required]
    [ForeignKey("FromLocation")]
    public int FromLocationID { get; set; }
    [Required]
    [ForeignKey("ToLocation")]
    public int ToLocationID { get; set; }
    public int? RequiredItemId { get; set; }
    [StringLength(100)]
    public string? Name { get; set; }

    [ForeignKey("RequiredItemId")]
    public virtual Item? RequiredItem { get; set; }

    public virtual Location FromLocation { get; set; } = null!;
    public virtual Location ToLocation { get; set; } = null!;
}
