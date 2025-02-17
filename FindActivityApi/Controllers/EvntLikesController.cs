using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindActivityApi.Models;
using FindActivityApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace FindActivityApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EvntLikesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public EvntLikesController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("{idEvent}/likesInfo")]
        public async Task<IActionResult> GetEventLikes(int idEvent)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var likeCount = await _context.EvntLikes
                .CountAsync(l => l.EvntId == idEvent);

            var userLiked = await _context.EvntLikes
                .AnyAsync(l => l.UserId == userId && l.EvntId == idEvent);

            return Ok(new
            {
                LikeCount = likeCount,
                UserLiked = userLiked
            });
        }

        [HttpPost("{idEvent}")]
        public void AddLike(int idEvent)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var evntLikes = new EvntLikes()
            {
                UserId = userId,
                EvntId = idEvent,
            };
            _context.EvntLikes.Add(evntLikes);
            _context.SaveChanges();
        }
        // DELETE: api/Comments/5
        [HttpDelete("{idEvent}")]
        public async Task<IActionResult> DeleteLike(int idEvent)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var like = await _context.EvntLikes.FirstOrDefaultAsync(l => l.UserId == userId && l.EvntId == idEvent);
            if (like == null)
            {
                return NotFound();
            }

            _context.EvntLikes.Remove(like);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}