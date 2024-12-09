using FindActivityApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class ActivityRequest
    {

        [Required]
        public string ActivityName { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

    }
}
