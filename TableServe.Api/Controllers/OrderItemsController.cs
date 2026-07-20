using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TableServe.Api.Data;
using TableServe.Api.Models;

namespace TableServe.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase {
        private readonly TableServeDbContext _db;

        public OrderItemsController(TableServeDbContext db) {
            _db = db;
        }

        private async Task RecalculateOrderTotal(int orderId) {
            var order = await _db.Orders.FindAsync(orderId);
            order!.Total = await _db.OrderItems
                                    .Where(oi => oi.OrderId == orderId)
                                    .SumAsync(oi => oi.Quantity * oi.MenuItem!.Price);
            await _db.SaveChangesAsync();
        }

        // GET: api/OrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAll() {
            return await _db.OrderItems
                            .Include(orderItem => orderItem.MenuItem)
                            .ToListAsync();
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetById(int id) {
            var orderItem = await _db.OrderItems
                                     .Include(orderItem => orderItem.MenuItem)
                                     .SingleOrDefaultAsync(orderItem => orderItem.Id == id);

            if (orderItem == null) {
                return NotFound();
            }

            return orderItem;
        }

        // POST: api/OrderItems
        [HttpPost]
        public async Task<ActionResult<OrderItem>> Create(OrderItem newOrderItem) {
            _db.OrderItems.Add(newOrderItem);
            await _db.SaveChangesAsync();
            await RecalculateOrderTotal(newOrderItem.OrderId);

            var orderItemWithMenuItem = await _db.OrderItems
                                                 .Include(oi => oi.MenuItem)
                                                 .SingleOrDefaultAsync(oi => oi.Id == newOrderItem.Id);

            return CreatedAtAction(nameof(GetById), new { id = newOrderItem.Id }, orderItemWithMenuItem);
        }

        // PUT: api/OrderItems/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderItem>> Update(int id, OrderItem updatedOrderItem) {
            if (id != updatedOrderItem.Id) {
                return BadRequest();
            }

            var currentOrderItem = await _db.OrderItems.FindAsync(id);
            if (currentOrderItem == null) {
                return NotFound();
            }

            _db.Entry(currentOrderItem).CurrentValues.SetValues(updatedOrderItem);
            await _db.SaveChangesAsync();
            await RecalculateOrderTotal(updatedOrderItem.OrderId);

            var orderItemWithMenuItem = await _db.OrderItems
                                                 .Include(oi => oi.MenuItem)
                                                 .SingleOrDefaultAsync(oi => oi.Id == id);

            return Ok(orderItemWithMenuItem);
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var orderItem = await _db.OrderItems.FindAsync(id);
            if (orderItem == null) {
                return NotFound();
            }

            var orderId = orderItem.OrderId;
            _db.OrderItems.Remove(orderItem);
            await _db.SaveChangesAsync();
            await RecalculateOrderTotal(orderId);

            return NoContent();
        }
    }
}
