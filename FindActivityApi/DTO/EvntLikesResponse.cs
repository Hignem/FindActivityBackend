using FindActivityApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FindActivityApi.DTO
{
    public class EvntLikesResponse
    {
        public int EvntLikesId { get; set; }

        public int EvntId { get; set; }

        public int UserId { get; set; }

    }
}
