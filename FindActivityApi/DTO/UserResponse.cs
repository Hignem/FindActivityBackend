using FindActivityApi.Models;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class UserResponse
    {
        public int UserId { get; set; }

        public String Name { get; set; } = "";


        public String Surname { get; set; } = "";

        //public string Email { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; } = null;
        public double? LatitudeX { get; set; }
        public double? LongitudeY { get; set; }
        public string ProfileImagePath { get; set; } = "";
    }
}
