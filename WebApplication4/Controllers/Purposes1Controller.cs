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
    [Route("[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class Purposes1Controller : ControllerBase
    {
        private readonly genieDBContext _context;

        public Purposes1Controller(genieDBContext context)
        {
            _context = context;
        }

        // GET: api/Purposes1
        [HttpGet]
        //[Route("Purposes1/GetPurposes")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerable<Purpose>>> GetPurposes()
        {
            return await _context.Purposes.ToListAsync();
           // return null;
        }

        // GET: api/Purposes1/5
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

        // PUT: api/Purposes1/5
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

        // POST: api/Purposes1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Purpose>> PostPurpose(Purpose purpose)
        {
            _context.Purposes.Add(purpose);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurpose", new { id = purpose.Id }, purpose);
        }

        // DELETE: api/Purposes1/5
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
