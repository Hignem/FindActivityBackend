using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindActivityApi.Models;
using FindActivityApi.DTO;

namespace FindActivityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivitiesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UserActivitiesController(ApiDbContext context)
        {
            _context = context;
        }
        private static UserActivityResponse toUserActivityResponse(UserActivity useractivity)
        {
            return new UserActivityResponse()
            {
                UserActivityId=useractivity.UserActivityId,
                UserId=useractivity.UserId,
                ActivityId=useractivity.ActivityId,
            };
        }
        // GET: api/UserActivities
        /*        [HttpGet]
                public async Task<ActionResult<IEnumerable<UserActivity>>> GetUserActivities()
                {
                    return await _context.UserActivities.ToListAsync();
                }*/
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserActivityResponse>>> GetUserActivities()
        {
            return await _context.UserActivities.Select(
                useractivity => toUserActivityResponse(useractivity)
                ).ToListAsync();
        }

        // GET: api/UserActivities/5
        /*        [HttpGet("{id}")]
                public async Task<ActionResult<UserActivity>> GetUserActivity(int id)
                {
                    var userActivity = await _context.UserActivities.FindAsync(id);

                    if (userActivity == null)
                    {
                        return NotFound();
                    }

                    return userActivity;
                }*/
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserActivityResponse>> GetUserActivity(int id)
        {
            var useractivity = await _context.UserActivities.FindAsync(id);

            if (useractivity == null)
            {
                return NotFound();
            }
            UserActivityResponse useractivityResponse = toUserActivityResponse(useractivity);
            return useractivityResponse;
        }
        // PUT: api/UserActivities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserActivity(int id, UserActivity userActivity)
        {
            if (id != userActivity.UserActivityId)
            {
                return BadRequest();
            }

            _context.Entry(userActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserActivityExists(id))
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

        // POST: api/UserActivities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*        [HttpPost]
                public async Task<ActionResult<UserActivity>> PostUserActivity(UserActivity userActivity)
                {
                    _context.UserActivities.Add(userActivity);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetUserActivity", new { id = userActivity.UserActivityId }, userActivity);
                }*/
        [HttpPost]
        public void PostUserActivity(UserActivityResponse useractivityRequest)
        {
            var useractivity = new UserActivity()
            {
                UserId = useractivityRequest.UserId,
                ActivityId = useractivityRequest.ActivityId,
            };
            _context.UserActivities.Add(useractivity);
            _context.SaveChanges();
        }
        // DELETE: api/UserActivities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserActivity(int id)
        {
            var userActivity = await _context.UserActivities.FindAsync(id);
            if (userActivity == null)
            {
                return NotFound();
            }

            _context.UserActivities.Remove(userActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserActivityExists(int id)
        {
            return _context.UserActivities.Any(e => e.UserActivityId == id);
        }
    }
}
