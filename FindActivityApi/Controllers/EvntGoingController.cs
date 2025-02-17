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
    public class EvntGoingController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public EvntGoingController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("{idEvent}/goingInfo")]
        public async Task<IActionResult> GetEventGoing(int idEvent)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var goingCount = await _context.EvntGoing
                .CountAsync(l => l.EvntId == idEvent);

            var userGoing = await _context.EvntGoing
                .AnyAsync(l => l.UserId == userId && l.EvntId == idEvent);

            return Ok(new
            {
                GoingCount = goingCount,
                UserGoing = userGoing
            });
        }

        [HttpPost("{idEvent}")]
        public void AddGoing(int idEvent)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var evntGoing = new EvntGoing()
            {
                UserId = userId,
                EvntId = idEvent,
            };
            _context.EvntGoing.Add(evntGoing);
            _context.SaveChanges();
        }
        // DELETE: api/Comments/5
        [HttpDelete("{idEvent}")]
        public async Task<IActionResult> DeleteGoing(int idEvent)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var going = await _context.EvntGoing.FirstOrDefaultAsync(l => l.UserId == userId && l.EvntId == idEvent);
            if (going == null)
            {
                return NotFound();
            }

            _context.EvntGoing.Remove(going);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}