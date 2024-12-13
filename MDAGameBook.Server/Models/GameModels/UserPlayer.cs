using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameBookASP.Models;

namespace GameBookASP.GameModels
{
    public class UserPlayer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Player")]
        public Guid PlayerId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public Player Player { get; set; } = null!;
    }
} 