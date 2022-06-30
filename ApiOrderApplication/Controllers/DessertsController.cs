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
    public class DessertsController : ControllerBase
    {
        private readonly ApiOrderApplicationContext _context;

        public DessertsController(ApiOrderApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Desserts
        [HttpGet]
        [ProducesResponseType(typeof(List<Dessert>), 200)]

        public async Task<ActionResult<IEnumerable<Dessert>>> GetDessert()
        {
          if (_context.Dessert == null)
          {
              return NotFound();
          }
            return await _context.Dessert.ToListAsync();
        }

        // GET: api/Desserts/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dessert), 200)]
        public async Task<ActionResult<Dessert>> GetDessert(long id)
        {
          if (_context.Dessert == null)
          {
              return NotFound();
          }
            var dessert = await _context.Dessert.FindAsync(id);

            if (dessert == null)
            {
                return NotFound();
            }

            return dessert;
        }

        // PUT: api/Desserts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutDessert(long id, Dessert dessert)
        {
            if (id != dessert.Id)
            {
                return BadRequest();
            }

            _context.Entry(dessert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DessertExists(id))
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

        // POST: api/Desserts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(Dessert), 201)]
        public async Task<ActionResult<Dessert>> PostDessert(Dessert dessert)
        {
          if (_context.Dessert == null)
          {
              return Problem("Entity set 'ApiOrderApplicationContext.Dessert'  is null.");
          }
            _context.Dessert.Add(dessert);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDessert", new { id = dessert.Id }, dessert);
        }

        // DELETE: api/Desserts/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDessert(long id)
        {
            if (_context.Dessert == null)
            {
                return NotFound();
            }
            var dessert = await _context.Dessert.FindAsync(id);
            if (dessert == null)
            {
                return NotFound();
            }

            _context.Dessert.Remove(dessert);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DessertExists(long id)
        {
            return (_context.Dessert?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
