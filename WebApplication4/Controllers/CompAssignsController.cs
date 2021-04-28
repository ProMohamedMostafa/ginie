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
    [EnableCors("AllowOrigin")]
    [Route("~/api/[controller]/[action]")]
    [ApiController]
    public class CompAssignsController : ControllerBase
    {
        private readonly genieDBContext _context;

        public CompAssignsController(genieDBContext context)
        {
            _context = context;
        }

        // GET: api/CompAssigns
        [HttpGet]
        [Route("~/api/CompAssigns/GetCompAssigns")]
        public async Task<ActionResult<IEnumerable<CompAssign>>> GetCompAssigns()
        {
            return await _context.CompAssigns.ToListAsync();
        }

        // GET: api/CompAssigns/5
        [HttpGet("{id}")]

        [Route("~/api/CompAssigns/GetCompAssign/{id}")]
        public async Task<ActionResult<CompAssign>> GetCompAssign(int id)
        {
            var compAssign = await _context.CompAssigns.FindAsync(id);

            if (compAssign == null)
            {
                return NotFound();
            }

            return compAssign;
        }

        // PUT: api/CompAssigns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Route("~/api/CompAssigns/PutCompAssign/{id}")]
        public async Task<IActionResult> PutCompAssign(int id, CompAssign compAssign)
        {
            if (id != compAssign.Id)
            {
                return BadRequest();
            }

            _context.Entry(compAssign).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompAssignExists(id))
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

        // POST: api/CompAssigns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors("AllowOrigin")]
        [Route("~/api/CompAssigns/PostCompAssign")]
        [HttpPost]

        public async Task<ActionResult<CompAssign>> PostCompAssign(CompAssign compAssign)
        {
            _context.CompAssigns.Add(compAssign);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompAssign", new { id = compAssign.Id }, compAssign);
        }

        // DELETE: api/CompAssigns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompAssign(int id)
        {
            var compAssign = await _context.CompAssigns.FindAsync(id);
            if (compAssign == null)
            {
                return NotFound();
            }

            _context.CompAssigns.Remove(compAssign);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompAssignExists(int id)
        {
            return _context.CompAssigns.Any(e => e.Id == id);
        }
    }
}
