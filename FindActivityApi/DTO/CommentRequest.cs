using FindActivityApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class CommentRequest
    {

        [ForeignKey("Evnt")]
        public int EvntId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string Content { get; set; }
    }
}
