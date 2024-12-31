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
    public bool? Condition { get; set; }

    public virtual Location? FromLocation { get; set; }
    public virtual Location? ToLocation { get; set; }
}
