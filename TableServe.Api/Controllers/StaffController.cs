using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TableServe.Api.Data;
using TableServe.Api.Models;

namespace TableServe.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase {
        private readonly TableServeDbContext _db;

        public StaffController(TableServeDbContext db) {
            _db = db;
        }

        // POST: api/Staff/login
        [HttpPost("login")]
        public async Task<ActionResult<Staff>> Login(Staff credentials) {
            var staff = await _db.Staff
                                .SingleOrDefaultAsync(staff => staff.Username == credentials.Username);

            if (staff == null || !BCrypt.Net.BCrypt.Verify(credentials.Password, staff.Password)) {
                return NotFound();
            }

            return staff;
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAll() {
            return await _db.Staff.ToListAsync();
        }

        // GET: api/Staff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetById(int id) {
            var staff = await _db.Staff.FindAsync(id);

            if (staff == null) {
                return NotFound();
            }

            return staff;
        }

        // POST: api/Staff
        [HttpPost]
        public async Task<ActionResult<Staff>> Create(Staff newStaff) {
            newStaff.Password = BCrypt.Net.BCrypt.HashPassword(newStaff.Password);
            _db.Staff.Add(newStaff);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newStaff.Id }, newStaff);
        }

        // PUT: api/Staff/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Staff>> Update(int id, Staff updatedStaff) {
            if (id != updatedStaff.Id) {
                return BadRequest();
            }

            var currentStaff = await _db.Staff.FindAsync(id);
            if (currentStaff == null) {
                return NotFound();
            }

            if (updatedStaff.Password != currentStaff.Password) {
                updatedStaff.Password = BCrypt.Net.BCrypt.HashPassword(updatedStaff.Password);
            }

            _db.Entry(currentStaff).CurrentValues.SetValues(updatedStaff);
            await _db.SaveChangesAsync();

            return Ok(currentStaff);
        }

        // DELETE: api/Staff/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var staff = await _db.Staff.FindAsync(id);
            if (staff == null) {
                return NotFound();
            }

            _db.Staff.Remove(staff);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
