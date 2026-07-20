using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TableServe.Api.Data;
using TableServe.Api.Models;

namespace TableServe.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase {
        private readonly TableServeDbContext _db;

        public OrdersController(TableServeDbContext db) {
            _db = db;
        }

        // GET: api/Orders
        // GET: api/Orders?status=PLACED
        // GET: api/Orders?status=PREPARING
        // GET: api/Orders?status=READY
        // GET: api/Orders?status=SERVED
        // GET: api/Orders?status=CANCELLED
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll([FromQuery] string? status = null) {
            var query = _db.Orders
                           .Include(order => order.Staff)
                           .AsQueryable();

            if (status != null) {
                query = query.Where(order => order.Status == status);
            }

            return await query.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(int id) {
            var order = await _db.Orders
                                .Include(order => order.Staff)
                                .Include(order => order.OrderItems)
                                    .ThenInclude(orderItem => orderItem.MenuItem)
                                .SingleOrDefaultAsync(order => order.Id == id);

            if (order == null) {
                return NotFound();
            }

            return order;
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> Create(Order newOrder) {
            _db.Orders.Add(newOrder);
            await _db.SaveChangesAsync();

            var orderWithStaff = await _db.Orders
                                          .Include(order => order.Staff)
                                          .SingleOrDefaultAsync(order => order.Id == newOrder.Id);

            return CreatedAtAction(nameof(GetById), new { id = newOrder.Id }, orderWithStaff);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Update(int id, Order updatedOrder) {
            if (id != updatedOrder.Id) {
                return BadRequest();
            }

            var currentOrder = await _db.Orders.FindAsync(id);
            if (currentOrder == null) {
                return NotFound();
            }

            _db.Entry(currentOrder).CurrentValues.SetValues(updatedOrder);
            await _db.SaveChangesAsync();

            var orderWithStaff = await _db.Orders
                                          .Include(order => order.Staff)
                                          .SingleOrDefaultAsync(order => order.Id == id);

            return Ok(orderWithStaff);
        }

        // PUT: api/Orders/5/startpreparing
        [HttpPut("{id}/startpreparing")]
        public async Task<ActionResult<Order>> StartPreparing(int id) {
            var currentOrder = await _db.Orders.FindAsync(id);
            if (currentOrder == null) {
                return NotFound();
            }

            currentOrder.Status = OrderStatus.Preparing;
            await _db.SaveChangesAsync();

            return Ok(currentOrder);
        }

        // PUT: api/Orders/5/markready
        [HttpPut("{id}/markready")]
        public async Task<ActionResult<Order>> MarkReady(int id) {
            var currentOrder = await _db.Orders.FindAsync(id);
            if (currentOrder == null) {
                return NotFound();
            }

            currentOrder.Status = OrderStatus.Ready;
            await _db.SaveChangesAsync();

            return Ok(currentOrder);
        }

        // PUT: api/Orders/5/markserved
        [HttpPut("{id}/markserved")]
        public async Task<ActionResult<Order>> MarkServed(int id) {
            var currentOrder = await _db.Orders.FindAsync(id);
            if (currentOrder == null) {
                return NotFound();
            }

            currentOrder.Status = OrderStatus.Served;
            await _db.SaveChangesAsync();

            return Ok(currentOrder);
        }

        // PUT: api/Orders/5/cancel
        [HttpPut("{id}/cancel")]
        public async Task<ActionResult<Order>> Cancel(int id, [FromBody] string cancellationReason) {
            var currentOrder = await _db.Orders.FindAsync(id);
            if (currentOrder == null) {
                return NotFound();
            }

            currentOrder.Status = OrderStatus.Cancelled;
            currentOrder.CancellationReason = cancellationReason;
            await _db.SaveChangesAsync();

            return Ok(currentOrder);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var order = await _db.Orders.FindAsync(id);
            if (order == null) {
                return NotFound();
            }

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
