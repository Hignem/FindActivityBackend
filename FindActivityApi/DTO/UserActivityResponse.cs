using FindActivityApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class UserActivityResponse
    {
        public int UserActivityId { get; set; }

        public int UserId { get; set; }
        
        public int ActivityId { get; set; }
    }
}
