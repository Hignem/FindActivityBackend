using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public String Name { get; set; } = "";
        
        [Required]
        public String Surname { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; } = null;
        public double? LatitudeX { get; set; }
        public double? LongitudeY { get; set; }
        public string ProfileImagePath { get; set; } = "";
        public ICollection<UserActivity> UserActivities { get; set; }
        public ICollection<Evnt> Evnts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
