using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public String Name { get; set; } = "";
        
        [Required]
        public String Surnname { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public String Password { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; } = null;
        public double? LatitudeX { get; set; }
        public double? LongitudeY { get; set; }
        public string ProfilePictureBase64 { get; set; } = "";
        public ICollection<UserActivity> UserActivities { get; set; }
        public ICollection<Evnt> Evnts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
