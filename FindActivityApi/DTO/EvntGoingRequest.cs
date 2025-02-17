using FindActivityApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class EvntGoingRequest
    {
        [ForeignKey("Evnt")]
        public int EvntId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
