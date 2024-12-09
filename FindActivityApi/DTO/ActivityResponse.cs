using FindActivityApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class ActivityResponse
    {
        public int ActivityId { get; set; }

        public string ActivityName { get; set; }

        public int CategoryId { get; set; }

    }
}
