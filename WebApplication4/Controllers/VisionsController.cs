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
    public class VisionsController : ControllerBase
    {
        private readonly genieDBContext _context;

        public VisionsController(genieDBContext context)
        {
            _context = context;
        }

        // GET: api/Visions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vision>>> GetVisions()
        {
            return await _context.Visions.ToListAsync();
        }

        // GET: api/Visions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vision>> GetVision(int id)
        {
            var vision = await _context.Visions.FindAsync(id);

            if (vision == null)
            {
                return NotFound();
            }

            return vision;
        }

        // PUT: api/Visions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVision(int id, Vision vision)
        {
            if (id != vision.Id)
            {
                return BadRequest();
            }

            _context.Entry(vision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisionExists(id))
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

        // POST: api/Visions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vision>> PostVision(Vision vision)
        {
            _context.Visions.Add(vision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVision", new { id = vision.Id }, vision);
        }

        // DELETE: api/Visions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVision(int id)
        {
            var vision = await _context.Visions.FindAsync(id);
            if (vision == null)
            {
                return NotFound();
            }

            _context.Visions.Remove(vision);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VisionExists(int id)
        {
            return _context.Visions.Any(e => e.Id == id);
        }
    }
}
