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

    public class EntriesController : ControllerBase
    {
        private readonly ApiOrderApplicationContext _context;

        public EntriesController(ApiOrderApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Entries
        [HttpGet]
        [ProducesResponseType(typeof(List<Entry>), 200)]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntry()
        {
          if (_context.Entry == null)
          {
              return NotFound();
          }
            return await _context.Entry.ToListAsync();
        }

        // GET: api/Entries/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Entry), 200)]
        public async Task<ActionResult<Entry>> GetEntry(long id)
        {
          if (_context.Entry == null)
          {
              return NotFound();
          }
            var entry = await _context.Entry.FindAsync(id);

            if (entry == null)
            {
                return NotFound();
            }

            return entry;
        }

        // PUT: api/Entries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutEntry(long id, Entry entry)
        {
            if (id != entry.Id)
            {
                return BadRequest();
            }

            _context.Entry(entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
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

        // POST: api/Entries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(Entry), 201)]
        public async Task<ActionResult<Entry>> PostEntry(Entry entry)
        {
          if (_context.Entry == null)
          {
              return Problem("Entity set 'ApiOrderApplicationContext.Entry'  is null.");
          }
            _context.Entry.Add(entry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntry", new { id = entry.Id }, entry);
        }

        // DELETE: api/Entries/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteEntry(long id)
        {
            if (_context.Entry == null)
            {
                return NotFound();
            }
            var entry = await _context.Entry.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            _context.Entry.Remove(entry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntryExists(long id)
        {
            return (_context.Entry?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
