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
    public class IndicatorsController : ControllerBase
    {
        private readonly genieDBContext _context;

        public IndicatorsController(genieDBContext context)
        {
            _context = context;
        }

        // GET: api/Indicators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Indicator>>> GetIndicators()
        {
            return await _context.Indicators.ToListAsync();
        }

        // GET: api/Indicators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Indicator>> GetIndicator(int id)
        {
            var indicator = await _context.Indicators.FindAsync(id);

            if (indicator == null)
            {
                return NotFound();
            }

            return indicator;
        }

        // PUT: api/Indicators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("~/api/[controller]/[action]/{id}")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Indicator>> PutIndicator(int id, Indicator indicator)
        {
            if (id != indicator.Id)
            {
                return BadRequest();
            }

            _context.Entry(indicator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndicatorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(indicator);
        }

        // POST: api/Indicators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Indicator>> PostIndicator(Indicator indicator)
        {
            _context.Indicators.Add(indicator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIndicator", new { id = indicator.Id }, indicator);
        }

        // DELETE: api/Indicators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndicator(int id)
        {
            var indicator = await _context.Indicators.FindAsync(id);
            if (indicator == null)
            {
                return NotFound();
            }

            _context.Indicators.Remove(indicator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IndicatorExists(int id)
        {
            return _context.Indicators.Any(e => e.Id == id);
        }
    }
}
