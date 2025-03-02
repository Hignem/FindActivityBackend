using FindActivityApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class EvntLikesResponse
    {
        public string ProfileImagePath { get; set; } = "";
        public string CreatedByFirstName { get; set; }
        public string CreatedBySurName { get; set; }

    }
}
