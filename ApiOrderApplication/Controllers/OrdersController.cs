using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiOrderApplication.Data;
using ApiOrderApplication.Models;

namespace ApiOrderApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApiOrderApplicationContext _context;

        public OrdersController(ApiOrderApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        [ProducesResponseType(typeof(List<Order>), 200)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
          if (_context.Order == null)
          {
              return NotFound();
          }
            return await _context.Order.Include(u => u.Entry).Include(u => u.Dish).Include(u => u.Dessert).Include(u => u.Drink).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Order), 200)]
        public async Task<ActionResult<Order>> GetOrder(long id)
        {
          if (_context.Order == null)
          {
              return NotFound();
          }
            var order = await _context.Order.Include(u => u.Entry).Include(u => u.Dish).Include(u => u.Dessert).Include(u => u.Drink).FirstOrDefaultAsync(p => p.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutOrder(long id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(Order), 201)]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_context.Order == null)
          {
              return Problem("Entity set 'ApiOrderApplicationContext.Order'  is null.");
          }
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            if (_context.Order == null)
            {
                return NotFound();
            }
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(long id)
        {
            return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
