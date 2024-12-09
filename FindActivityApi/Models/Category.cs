using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
