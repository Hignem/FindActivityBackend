using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class CategoryRequest
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
