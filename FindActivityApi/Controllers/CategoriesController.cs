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
using System.Text.Json;

namespace FindActivityApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CategoriesController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpPost("addcategorywithactivities")]
        public async Task<IActionResult> AddCategoryWithActivities([FromBody] CategoryWithActivitiesRequest request)
        {
            // Wywołanie procedury składowanej
            await _context.Database.ExecuteSqlRawAsync(
                "CALL AddCategoryWithActivities({0}, {1})",
                request.CategoryName,
                request.Activities.ToArray()
            );

            return Ok(new { message = "Kategoria i aktywności zostały pomyślnie dodane." });
        }
        private static CategoryResponse toCategoryResponse(Category category)
        {
            return new CategoryResponse()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }
        // GET: api/Categories
        /*        [HttpGet]
                public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
                {
                    return await _context.Categories.ToListAsync();
                }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetCategories()
        {
            return await _context.Categories.Select(
                category => toCategoryResponse(category)).ToListAsync();
        }
        // GET: api/Categories/5
        /*        [HttpGet("{id}")]
                public async Task<ActionResult<Category>> GetCategory(int id)
                {
                    var category = await _context.Categories.FindAsync(id);

                    if (category == null)
                    {
                        return NotFound();
                    }

                    return category;
                }*/
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            CategoryResponse categoryResponse = toCategoryResponse(category);
            return categoryResponse;
        }
        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryRequest categoryRequest)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.CategoryName = categoryRequest.CategoryName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
/*        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }*/

        [HttpPost]
        public void PostCategory(CategoryRequest categoryRequest)
        {
            var category = new Category()
            {
                CategoryName = categoryRequest.CategoryName
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
