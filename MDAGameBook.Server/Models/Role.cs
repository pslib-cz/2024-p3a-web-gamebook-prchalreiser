using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBookASP.Models {
    [Table("AspNetRoles")]
    public class Role : IdentityRole<string> {
        public ICollection<User>? Users { get; set; }
    }
}
