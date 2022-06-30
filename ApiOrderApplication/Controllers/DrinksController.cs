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
    public class DrinksController : ControllerBase
    {
        private readonly ApiOrderApplicationContext _context;

        public DrinksController(ApiOrderApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Drinks
        [HttpGet]
        [ProducesResponseType(typeof(List<Drink>), 200)]
        public async Task<ActionResult<IEnumerable<Drink>>> GetDrink()
        {
          if (_context.Drink == null)
          {
              return NotFound();
          }
            return await _context.Drink.ToListAsync();
        }

        // GET: api/Drinks/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Drink), 200)]
        public async Task<ActionResult<Drink>> GetDrink(long id)
        {
          if (_context.Drink == null)
          {
              return NotFound();
          }
            var drink = await _context.Drink.FindAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            return drink;
        }

        // PUT: api/Drinks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutDrink(long id, Drink drink)
        {
            if (id != drink.Id)
            {
                return BadRequest();
            }

            _context.Entry(drink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(id))
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

        // POST: api/Drinks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(Drink), 201)]
        public async Task<ActionResult<Drink>> PostDrink(Drink drink)
        {
          if (_context.Drink == null)
          {
              return Problem("Entity set 'ApiOrderApplicationContext.Drink'  is null.");
          }
            _context.Drink.Add(drink);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrink", new { id = drink.Id }, drink);
        }

        // DELETE: api/Drinks/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDrink(long id)
        {
            if (_context.Drink == null)
            {
                return NotFound();
            }
            var drink = await _context.Drink.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            _context.Drink.Remove(drink);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DrinkExists(long id)
        {
            return (_context.Drink?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
