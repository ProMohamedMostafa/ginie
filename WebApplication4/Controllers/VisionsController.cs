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
    public class VisionsController : ControllerBase
    {
        private readonly genieDBContext _context;
        VisionsLogic _visionsLogic;

        public VisionsController(genieDBContext context)
        {
            _context = context;
            _visionsLogic = new VisionsLogic(_context);
        }

        // GET: api/Visions
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Vision>>> GetVisions()
        {
            try
            {
                var visions = await _visionsLogic.GetVisions();
                return visions;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Visions/5
        [HttpPost("{id}")]
        public async Task<ActionResult<Vision>> GetVision(int id)
        {
            try
            {
                var vision = await _visionsLogic.GetVision(id);

                if (vision == null)
                {
                    return NotFound();
                }

                return vision;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Visions/5
        [HttpPost("{id}")]
        public async Task<IActionResult> PutVision(int id, Vision vision)
        {
            try
            {
                if (id != vision.Id)
                {
                    return BadRequest();
                }
                var comAss = await _visionsLogic.PutVision(id, vision);
                return Ok(vision);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!VisionExists(id))
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

        // POST: api/Visions
        [HttpPost]
        public async Task<ActionResult<Vision>> PostVision(Vision vision)
        {
            try
            {
                var NewVision =await _visionsLogic.PostVision(vision);
                return Ok(NewVision);
                    }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Visions/5
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteVision(int id)
        {
            try
            {
                var vision = await _visionsLogic.DeleteVision(id);
                if (vision == false)
                {
                    return Ok("Vision Deleted Filled");
                }

                return Ok("Vision Deleted success");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // check if vision Exist
        private bool VisionExists(int id)
        {
            try
            {
                var visionExists = _visionsLogic.VisionExists(id);
                return visionExists;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
