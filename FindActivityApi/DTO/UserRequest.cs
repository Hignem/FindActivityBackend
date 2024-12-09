using FindActivityApi.Models;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class UserRequest
    {
        [Required]
        public String Name { get; set; } = "";

        [Required]
        public String Surname { get; set; } = "";

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; } = "";

        //[Required]
        //public String Password { get; set; } = "";

    }
}
