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
    public class DishesController : ControllerBase
    {
        private readonly ApiOrderApplicationContext _context;

        public DishesController(ApiOrderApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Dishes
        [HttpGet]
        [ProducesResponseType(typeof(List<Dish>), 200)]
        public async Task<ActionResult<IEnumerable<Dish>>> GetDish()
        {
          if (_context.Dish == null)
          {
              return NotFound();
          }
            return await _context.Dish.ToListAsync();
        }

        // GET: api/Dishes/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dish), 200)]
        public async Task<ActionResult<Dish>> GetDish(long id)
        {
          if (_context.Dish == null)
          {
              return NotFound();
          }
            var dish = await _context.Dish.FindAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            return dish;
        }

        // PUT: api/Dishes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutDish(long id, Dish dish)
        {
            if (id != dish.Id)
            {
                return BadRequest();
            }

            _context.Entry(dish).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
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

        // POST: api/Dishes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(Dish), 201)]
        public async Task<ActionResult<Dish>> PostDish(Dish dish)
        {
          if (_context.Dish == null)
          {
              return Problem("Entity set 'ApiOrderApplicationContext.Dish'  is null.");
          }
            _context.Dish.Add(dish);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDish", new { id = dish.Id }, dish);
        }

        // DELETE: api/Dishes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDish(long id)
        {
            if (_context.Dish == null)
            {
                return NotFound();
            }
            var dish = await _context.Dish.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }

            _context.Dish.Remove(dish);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DishExists(long id)
        {
            return (_context.Dish?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
