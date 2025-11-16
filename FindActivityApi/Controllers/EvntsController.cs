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

        private static EvntResponse toEvntResponse(Evnt evnt, int currentUserId)
        {
            return new EvntResponse()
            {
                EvntId = evnt.EvntId,
                UserId = evnt.UserId,
                ActivityId = evnt.ActivityId,
                CategoryId = evnt.Activity.CategoryId,
                Title = evnt.Title,
                Content = evnt.Content,
                CreatedAt = evnt.CreatedAt,
                DateOfEvnt = evnt.DateOfEvnt,
                EvntImagePath = evnt.EvntImagePath,
                LatitudeX = evnt.LatitudeX,
                LongitudeY = evnt.LongitudeY,
                CreatedByFirstName = evnt.User.Name,
                CreatedByLastName = evnt.User.Surname,
                ProfileImagePath = evnt.User.ProfileImagePath,
                IsOwner = evnt.UserId == currentUserId,
            };
        }
        //// imp!!!
        //[HttpGet("Favourites/{sortBy}")]
        //public async Task<IActionResult> GetFavouritesEvents(string sortBy)
        //{

        //    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        //    var observedEvents = _context.Evnts
        //        .Where(e => _context.UserActivities
        //            .Any(ua => ua.UserId == userId && ua.ActivityId == e.ActivityId))
        //        .Select(e => new EvntResponse
        //        {
        //            EvntId = e.EvntId,
        //            UserId = e.UserId,
        //            ActivityId = e.ActivityId,
        //            Title = e.Title,
        //            Content = e.Content,
        //            CreatedAt = e.CreatedAt,
        //            DateOfEvnt = e.DateOfEvnt,
        //            EvntImagePath = e.EvntImagePath,
        //            LatitudeX = e.LatitudeX,
        //            LongitudeY = e.LongitudeY,
        //            CreatedByFirstName = e.User.Name,
        //            CreatedByLastName = e.User.Surname,
        //            ProfileImagePath = e.User.ProfileImagePath
        //        });

        //    if (sortBy == "newest")
        //    {
        //        observedEvents = observedEvents.OrderByDescending(e => e.CreatedAt);
        //    }
        //    if (sortBy == "closetoyou")
        //    {
        //        observedEvents = observedEvents.OrderByDescending(e => e.CreatedAt);
        //    }
        //    else if (sortBy == "upcoming")
        //    {
        //        observedEvents = observedEvents
        //            .Where(e => e.DateOfEvnt >= DateTime.UtcNow)
        //            .OrderBy(e => e.DateOfEvnt);
        //    }

        //    var observedEventsEnd = await observedEvents.ToListAsync();

        //    return Ok(observedEventsEnd);
        //}

        // imp!!!
        [HttpGet("Favourites/{sortBy}")]
        public async Task<IActionResult> GetFavouritesEvents(string sortBy)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var query = _context.Evnts
                .Include(e => e.User)
                .Include(e => e.Activity)
                .Where(e => _context.UserActivities
                    .Any(ua => ua.UserId == userId && ua.ActivityId == e.ActivityId))
                .AsQueryable();

            if (sortBy == "newest")
            {
                query = query.OrderByDescending(e => e.CreatedAt);
            }
            else if (sortBy == "closetoyou")
            {
                query = query.OrderByDescending(e => e.CreatedAt);
            }
            else if (sortBy == "upcoming")
            {
                query = query
                    .Where(e => e.DateOfEvnt >= DateTime.UtcNow)
                    .OrderBy(e => e.DateOfEvnt);
            }

            var result = await query
                .Select(e => toEvntResponse(e, userId))
                .ToListAsync();

            return Ok(result);
        }

        //imp!!!
        [HttpGet("Search")]
        public async Task<IActionResult> SearchEvents(string? title, string? activityIds)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var query = _context.Evnts
                .Include(e => e.User)
                                .Include(e => e.Activity)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                var loweredTitle = title.ToLower();
                query = query.Where(e => e.Title.ToLower().Contains(loweredTitle));
            }

            if (!string.IsNullOrWhiteSpace(activityIds))
            {
                var ids = activityIds.Split(',')
                                     .Select(int.Parse)
                                     .ToList();
                query = query.Where(e => ids.Contains(e.ActivityId));
            }

            var result = await query
                .Select(e => toEvntResponse(e, userId))
                .ToListAsync();

            return Ok(result);
        }


        //// GET: api/Evnts
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<EvntResponse>>> GetEvnts()
        //{
        //    return await _context.Evnts.Select(
        //        evnt => toEvntResponse(evnt)
        //        ).ToListAsync();
        //}
        //// GET: api/Evnts/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<EvntResponse>> GetEvnt(int id)
        //{
        //    var evnt = await _context.Evnts.FindAsync(id);

        //    if (evnt == null)
        //    {
        //        return NotFound();
        //    }
        //    EvntResponse evntResponse = toEvntResponse(evnt);
        //    return evntResponse;
        //}
      

        // imp!!!
        // POST: api/Evnts
        [HttpPost]
        public IActionResult PostEvnt(EvntRequest evntRequest)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var evnt = new Evnt()
            {
                UserId = userId,
                ActivityId = evntRequest.ActivityId,
                Title = evntRequest.Title,
                Content = evntRequest.Content,
                DateOfEvnt = evntRequest.DateOfEvnt,
                LatitudeX = evntRequest.LatitudeX,
                LongitudeY = evntRequest.LongitudeY,

            };
            _context.Evnts.Add(evnt);
            _context.SaveChanges();

            return Ok(evnt.EvntId);
        }

        // imp!!!
        [HttpDelete("{evntId}")]
        public async Task<IActionResult> DeleteEvnt(int evntId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var evnt = await _context.Evnts.FindAsync(evntId);
            if (evnt == null)
            {
                return NotFound("Wydarzenie nie zostało znalezione.");
            }

            if (evnt.UserId != userId)
            {
                return Forbid("Nie masz uprawnień do usunięcia tego wydarzenia.");
            }

            _context.Evnts.Remove(evnt);
            await _context.SaveChangesAsync();

            return Ok("Wydarzenie zostało usunięte.");
        }

        // imp!!!
        [HttpPut("{evntId}")]
        public async Task<IActionResult> UpdateEvnt(int evntId, [FromBody] EvntRequest evntRequest)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var evnt = await _context.Evnts.FindAsync(evntId);
            if (evnt == null)
            {
                return NotFound("Wydarzenie nie zostało znalezione.");
            }

            if (evnt.UserId != userId)
            {
                return Forbid("Nie masz uprawnień do edycji tego wydarzenia.");
            }

            evnt.ActivityId = evntRequest.ActivityId;
            evnt.Title = evntRequest.Title;
            evnt.Content = evntRequest.Content;
            evnt.DateOfEvnt = evntRequest.DateOfEvnt;
            evnt.LatitudeX = evntRequest.LatitudeX;
            evnt.LongitudeY = evntRequest.LongitudeY;

            await _context.SaveChangesAsync();

            return Ok("Wydarzenie zostało zaktualizowane.");
        }
        // imp!!!
        [HttpPut("upload-event-image")]
        public async Task<IActionResult> UploadEventImage([FromForm] int evntId, IFormFile file)
        {


            if (file == null || file.Length == 0)
            {
                return BadRequest("Plik jest pusty.");
            }

            // Tworzenie folderu jeśli nie istnieje
            var uploadsFolder = Path.Combine("wwwroot", "images", "evnts");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imagePath = $"/images/evnts/{fileName}";

            // Pobieranie wydarzenia z bazy
            var evnt = await _context.Evnts.FindAsync(evntId);
            if (evnt == null)
            {
                return NotFound("Wydarzenie nie zostało znalezione.");
            }

            // Usuwanie starego zdjęcia
            if (!string.IsNullOrEmpty(evnt.EvntImagePath))
            {
                var oldImagePath = Path.Combine("wwwroot", evnt.EvntImagePath.TrimStart('/'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    try
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    catch (Exception)
                    {
                        return StatusCode(500, "Błąd przy usuwaniu starego zdjęcia.");
                    }
                }
            }

            // Zapis nowej ścieżki
            evnt.EvntImagePath = imagePath;
            await _context.SaveChangesAsync();

            return Ok(new { imagePath });
        }
        // imp!!!
        [HttpDelete("delete-event-image/{evntId}")]
        public async Task<IActionResult> DeleteEventImage(int evntId)
        {
            var evnt = await _context.Evnts.FindAsync(evntId);
            if (evnt == null)
            {
                return NotFound("Wydarzenie nie zostało znalezione.");
            }

            //if (string.IsNullOrEmpty(evnt.EvntImagePath))
            //{
            //    return BadRequest("To wydarzenie nie ma przypisanego zdjęcia.");
            //}
            var fullImagePath = Path.Combine("wwwroot", evnt.EvntImagePath.TrimStart('/'));
            if (System.IO.File.Exists(fullImagePath))
            {
                try
                {
                    System.IO.File.Delete(fullImagePath);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Błąd przy usuwaniu pliku: {ex.Message}");
                }
            }

            evnt.EvntImagePath = "";
            await _context.SaveChangesAsync();

            return Ok("Zdjęcie zostało usunięte.");
        }
    }


}
