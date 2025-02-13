using FindActivityApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class EvntResponse
    {
        public int EvntId { get; set; }

        public int UserId { get; set; }

        public int ActivityId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string EvntImagePath { get; set; } = "";
        public string CreatedByFirstName { get; set; }
        public string CreatedByLastName { get; set; }
        public string ProfileImagePath { get; set; } = "";


    }
}
