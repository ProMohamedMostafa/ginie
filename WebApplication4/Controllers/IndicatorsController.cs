using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GenieMistro.Models;
using Microsoft.AspNetCore.Cors;
using GenieMistro.BL;

namespace GenieMistro.Controllers
{
    [Route("~/api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class IndicatorsController : ControllerBase
    {
        private readonly genieDBContext _context;
        IndicatorsLogic _indicatorsLogic;

        public IndicatorsController(genieDBContext context)
        {
            _context = context;
            _indicatorsLogic = new IndicatorsLogic(_context);
        }

        // GET: api/Indicators
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Indicator>>> GetIndicators()
        {
            try
            {
                var indicators = await _indicatorsLogic.GetIndicators();
                return indicators;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Indicators/5
        [HttpPost("{id}")]
        public async Task<ActionResult<Indicator>> GetIndicator(int id)
        {
            try
            {
                var indicator = await _indicatorsLogic.GetIndicator(id);

                if (indicator == null)
                {
                    return NotFound();
                }

                return indicator;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Indicators/5
        [Route("~/api/[controller]/[action]/{id}")]
        [HttpPost("{id}")]
        public async Task<ActionResult<Indicator>> PutIndicator(int id, Indicator indicator)
        {
            try
            {
                if (id != indicator.Id)
                {
                    return BadRequest();
                }
                var comAss = await _indicatorsLogic.PutIndicator(id, indicator);
                return Ok(indicator);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!IndicatorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Indicators
        [HttpPost]
        public async Task<ActionResult<Indicator>> PostIndicator(Indicator indicator)
        {
            try
            {
                var NewIndicator =await _indicatorsLogic.PostIndicator(indicator);
                return Ok(NewIndicator);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Indicators/5
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteIndicator(int id)
        {
            try
            {
                var indicator = await _indicatorsLogic.DeleteIndicator(id);
                if (indicator == false)
                {
                    return Ok("Indicator Deleted Filled");
                }

                return Ok("Indicator Deleted success");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private bool IndicatorExists(int id)
        {
            try
            {
                var IndicatorExist = _indicatorsLogic.IndicatorExists(id);
                return IndicatorExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
