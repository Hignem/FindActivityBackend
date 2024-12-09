using FindActivityApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class EvntRequest
    {

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Activity")]
        public int ActivityId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

    }
}
