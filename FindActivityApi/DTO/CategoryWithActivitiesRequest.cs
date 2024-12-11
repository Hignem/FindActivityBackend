using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class CategoryWithActivitiesRequest
    {
        [Required]
        public string CategoryName { get; set; }

        [Required]
        public List<string> Activities { get; set; }
    }
}
