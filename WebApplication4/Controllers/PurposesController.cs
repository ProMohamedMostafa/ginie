using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GenieMistro.Models;
using Microsoft.AspNetCore.Cors;

namespace GenieMistro.Controllers
{
    [Route("~/api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class PurposesController : ControllerBase
    {
        private readonly genieDBContext _context;

        public PurposesController(genieDBContext context)
        {
            _context = context;
        }

        // GET: api/Purposes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purpose>>> GetPurposes()
        {
            return await _context.Purposes.ToListAsync();
        }

        // GET: api/Purposes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Purpose>> GetPurpose(int id)
        {
            var purpose = await _context.Purposes.FindAsync(id);

            if (purpose == null)
            {
                return NotFound();
            }

            return purpose;
        }

        // PUT: api/Purposes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurpose(int id, Purpose purpose)
        {
            if (id != purpose.Id)
            {
                return BadRequest();
            }

            _context.Entry(purpose).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurposeExists(id))
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

        // POST: api/Purposes
       
        [HttpPost]
        public async Task<ActionResult<Purpose>> PostPurpose(Purpose purpose)
        {
            _context.Purposes.Add(purpose);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurpose", new { id = purpose.Id }, purpose);
        }

        // DELETE: api/Purposes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurpose(int id)
        {
            var purpose = await _context.Purposes.FindAsync(id);
            if (purpose == null)
            {
                return NotFound();
            }

            _context.Purposes.Remove(purpose);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurposeExists(int id)
        {
            return _context.Purposes.Any(e => e.Id == id);
        }
    }
}
