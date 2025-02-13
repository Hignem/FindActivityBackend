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
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace FindActivityApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EvntsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public EvntsController(ApiDbContext context)
        {
            _context = context;
        }
        private static EvntResponse toEvntResponse(Evnt evnt)
        {
            return new EvntResponse()
            {
                EvntId = evnt.EvntId,
                UserId = evnt.UserId,
                ActivityId = evnt.ActivityId,
                Title = evnt.Title,
                Content = evnt.Content, 
                CreatedAt = evnt.CreatedAt

            };
        }
        [HttpGet("Favourites")]
        public async Task<IActionResult> GetFavouritesEvents()
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var observedEvents = await _context.Evnts
                .Where(e => _context.UserActivities
                    .Any(ua => ua.UserId == userId && ua.ActivityId == e.ActivityId))
                .Select(e => new EvntResponse
                {
                    EvntId = e.EvntId,
                    UserId = e.UserId,
                    ActivityId = e.ActivityId,
                    Title = e.Title,
                    Content = e.Content,
                    CreatedAt = e.CreatedAt,
                    EvntImagePath = e.EvntImagePath,
                    CreatedByFirstName = e.User.Name,
                    CreatedByLastName = e.User.Surname,
                    ProfileImagePath = e.User.ProfileImagePath
                })
                .ToListAsync();

            return Ok(observedEvents);
        }

        // GET: api/Evnts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvntResponse>>> GetEvnts()
        {
            return await _context.Evnts.Select(
                evnt => toEvntResponse(evnt)
                ).ToListAsync();
        }
        // GET: api/Evnts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvntResponse>> GetEvnt(int id)
        {
            var evnt = await _context.Evnts.FindAsync(id);

            if (evnt == null)
            {
                return NotFound();
            }
            EvntResponse evntResponse = toEvntResponse(evnt);
            return evntResponse;
        }
        // PUT: api/Evnts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvnt(int id, EvntRequest evntRequest)
        {
            var evnt = await _context.Evnts.FindAsync(id);
            if (evnt == null)
            {
                return NotFound();
            }

            evnt.UserId = evntRequest.UserId;
            evnt.ActivityId = evntRequest.ActivityId;
            evnt.Title = evntRequest.Title;
            evnt.Content = evntRequest.Content;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvntExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // POST: api/Evnts
        [HttpPost]
        public void PostEvnt(EvntRequest evntRequest)
        {
            var evnt = new Evnt()
            {
                UserId = evntRequest.UserId,
                ActivityId = evntRequest.ActivityId,
                Title = evntRequest.Title,
                Content = evntRequest.Content,
            };
            _context.Evnts.Add(evnt);
            _context.SaveChanges();
        }
        // DELETE: api/Evnts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvnt(int id)
        {
            var evnt = await _context.Evnts.FindAsync(id);
            if (evnt == null)
            {
                return NotFound();
            }

            _context.Evnts.Remove(evnt);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool EvntExists(int id)
        {
            return _context.Evnts.Any(e => e.EvntId == id);
        }
    }


}
