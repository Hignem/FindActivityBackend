using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.Models
{
    public class EvntGoing
    {
        [Key]
        public int EvntGoingId { get; set; }

        [ForeignKey("Evnt")]
        public int EvntId { get; set; }
        public Evnt Evnt { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
