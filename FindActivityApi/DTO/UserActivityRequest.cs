using FindActivityApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class UserActivityRequest
    {
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
    }
}
