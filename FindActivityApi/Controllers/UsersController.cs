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
using System.Xml.Linq;
using System.Security.Claims;

namespace FindActivityApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UsersController(ApiDbContext context)
        {
            _context = context;
        }
        private static UserResponse toUserResponse(User user)
        {
            return new UserResponse()
            {
                Name = user.Name,
                Surname = user.Surname,
                CreatedAt = user.CreatedAt,
                //Email = user.Email,
                LastLogin = user.LastLogin,
                LatitudeX = user.LatitudeX,
                LongitudeY = user.LongitudeY,
                ProfileImagePath = user.ProfileImagePath,
                UserId = user.UserId

            };
        }
        [HttpPost("upload-profile-image")]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            // sprawdzenie czy uzytkownik przeslal zdjecie
            if (file == null || file.Length == 0)
            {
                return BadRequest("Plik jest pusty.");
            }

            //przypisanie docelowej sciezki
            var uploadsFolder = Path.Combine("wwwroot", "images", "profiles");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imagePath = $"/images/profiles/{fileName}";

            // Zapisujemy ścieżkę w bazie
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("Użytkownik nie znaleziony.");
            }

            //deleting old profile picture
            if (!string.IsNullOrEmpty(user.ProfileImagePath))
            {
                var oldImagePath = Path.Combine("wwwroot", user.ProfileImagePath.TrimStart('/'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    try
                    {
                        System.IO.File.Delete(oldImagePath); 
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, "Błąd przy usuwaniu starego zdjęcia.");
                    }
                }
            }
            user.ProfileImagePath = imagePath;
            await _context.SaveChangesAsync();

            return Ok(new { imagePath });
        }

        // GET: api/Users
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        //{
        //    return await _context.Users.ToListAsync();
        //}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            return await _context.Users.Select(
                user => toUserResponse(user)
                ).ToListAsync();
        }

        // GET: api/Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return user;
        //}

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            UserResponse userResponse = toUserResponse(user);
            return userResponse;
        }



        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserRequest userRequest)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = userRequest.Name;
            user.Surname = userRequest.Surname;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        //}
/*        [HttpPost]
        public void PostUser(UserRequest userRequest)
        {
            var user = new User() { 
            Name = userRequest.Name,
            //Email = userRequest.Email,
            //Password = userRequest.Password,
            Surname = userRequest.Surname,
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }*/

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
