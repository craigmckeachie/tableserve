using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TableServe.Api.Data;
using TableServe.Api.Models;

namespace TableServe.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase {
        private readonly TableServeDbContext _db;

        public MenuItemsController(TableServeDbContext db) {
            _db = db;
        }

        // GET: api/MenuItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetAll() {
            return await _db.MenuItems
                            .Include(menuItem => menuItem.Category)
                            .ToListAsync();
        }

        // GET: api/MenuItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetById(int id) {
            var menuItem = await _db.MenuItems
                                   .Include(menuItem => menuItem.Category)
                                   .SingleOrDefaultAsync(menuItem => menuItem.Id == id);

            if (menuItem == null) {
                return NotFound();
            }

            return menuItem;
        }

        // POST: api/MenuItems
        [HttpPost]
        public async Task<ActionResult<MenuItem>> Create(MenuItem newMenuItem) {
            _db.MenuItems.Add(newMenuItem);
            await _db.SaveChangesAsync();

            var menuItemWithCategory = await _db.MenuItems
                                             .Include(menuItem => menuItem.Category)
                                             .SingleOrDefaultAsync(menuItem => menuItem.Id == newMenuItem.Id);

            return CreatedAtAction(nameof(GetById), new { id = newMenuItem.Id }, menuItemWithCategory);
        }

        // PUT: api/MenuItems/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MenuItem>> Update(int id, MenuItem updatedMenuItem) {
            if (id != updatedMenuItem.Id) {
                return BadRequest();
            }

            var currentMenuItem = await _db.MenuItems.FindAsync(id);
            if (currentMenuItem == null) {
                return NotFound();
            }

            _db.Entry(currentMenuItem).CurrentValues.SetValues(updatedMenuItem);
            await _db.SaveChangesAsync();

            var menuItemWithCategory = await _db.MenuItems
                                             .Include(menuItem => menuItem.Category)
                                             .SingleOrDefaultAsync(menuItem => menuItem.Id == id);

            return Ok(menuItemWithCategory);
        }

        // DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var menuItem = await _db.MenuItems.FindAsync(id);
            if (menuItem == null) {
                return NotFound();
            }

            _db.MenuItems.Remove(menuItem);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
