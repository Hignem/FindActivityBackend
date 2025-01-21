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

namespace FindActivityApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ActivitiesController(ApiDbContext context)
        {
            _context = context;
        }

        private static ActivityResponse toActivityResponse(Activity activity)
        {
            return new ActivityResponse()
            {
                ActivityId = activity.ActivityId,
                ActivityName = activity.ActivityName,
                CategoryId = activity.CategoryId
            };
        }

        // GET: api/Activities
        /*        [HttpGet]
                public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
                {
                    return await _context.Activities.ToListAsync();
                }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityResponse>>> GetActivities()
        {
            return await _context.Activities.Select(
                activity => toActivityResponse(activity)).ToListAsync();
        }

        // GET: api/Activities/5
        /*        [HttpGet("{id}")]
                public async Task<ActionResult<Activity>> GetActivity(int id)
                {
                    var activity = await _context.Activities.FindAsync(id);

                    if (activity == null)
                    {
                        return NotFound();
                    }

                    return activity;
                }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityResponse>> GetActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            ActivityResponse activityResponse = toActivityResponse(activity);
            return activityResponse;
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutActivity(int id, Activity activity)
        //{
        //    if (id != activity.ActivityId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(activity).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ActivityExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, ActivityRequest activityRequest)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            activity.ActivityName = activityRequest.ActivityName;
            activity.CategoryId = activityRequest.CategoryId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
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
        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*        [HttpPost]
                public async Task<ActionResult<Activity>> PostActivity(Activity activity)
                {
                    _context.Activities.Add(activity);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetActivity", new { id = activity.ActivityId }, activity);
                }*/

        [HttpPost]
        public void PostActivity(ActivityRequest activityRequest)
        {
            var activity = new Activity()
            {
                ActivityName = activityRequest.ActivityName,
                CategoryId = activityRequest.CategoryId
            };
            _context.Activities.Add(activity);
            _context.SaveChanges();
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(e => e.ActivityId == id);
        }
    }
}
