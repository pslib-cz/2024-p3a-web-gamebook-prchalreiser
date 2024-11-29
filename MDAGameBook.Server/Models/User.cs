using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GameBookASP.Models {
    [Table("AspNetUsers")]
    public class User : IdentityUser<string> {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public ICollection<Role>? Roles { get; set; }
    }
}
