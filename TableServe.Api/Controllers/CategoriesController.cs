using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TableServe.Api.Data;
using TableServe.Api.Models;

namespace TableServe.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        private readonly TableServeDbContext _db;

        public CategoriesController(TableServeDbContext db) {
            _db = db;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll() {
            return await _db.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id) {
            var category = await _db.Categories.FindAsync(id);

            if (category == null) {
                return NotFound();
            }

            return category;
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category newCategory) {
            _db.Categories.Add(newCategory);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Update(int id, Category updatedCategory) {
            if (id != updatedCategory.Id) {
                return BadRequest();
            }

            var currentCategory = await _db.Categories.FindAsync(id);
            if (currentCategory == null) {
                return NotFound();
            }

            _db.Entry(currentCategory).CurrentValues.SetValues(updatedCategory);
            await _db.SaveChangesAsync();

            return Ok(currentCategory);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var category = await _db.Categories.FindAsync(id);
            if (category == null) {
                return NotFound();
            }

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
