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
    public class PurposesController : ControllerBase
    {
        private readonly genieDBContext _context;
        PurposesLogic _purposesLogic;
        public PurposesController(genieDBContext context)
        {
            _context = context;
            _purposesLogic = new PurposesLogic(_context);
        }

        // GET: api/Purposes
       // [HttpGet]
       [HttpPost]
        public async Task<ActionResult<IEnumerable<Purpose>>> GetPurposes()
        {
            try
            {
                var purposes = await _purposesLogic.GetPurposes();
                return purposes;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Purposes/5
        //[HttpGet("{id}")]
        [HttpPost("{id}")]
        public async Task<ActionResult<Purpose>> GetPurpose(int id)
        {
            try
            {
                var purpose = await _purposesLogic.GetPurpose(id);

                if (purpose == null)
                {
                    return NotFound();
                }

                return purpose;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Purposes/5
      //  [HttpPut("{id}")]
      [HttpPost("{id}")]
        public async Task<IActionResult> PutPurpose(int id, Purpose purpose)
        {
            try
            {
                if (id != purpose.Id)
                {
                    return BadRequest();
                }
                var comAss = await _purposesLogic.PutPurpose(id, purpose);
                return Ok(purpose);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PurposeExists(id))
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

        // POST: api/Purposes       
        [HttpPost]
        public async Task< IActionResult> PostPurpose(Purpose purpose)
        {
            try
            {
                var NewPurpose =await _purposesLogic.PostPurpose(purpose);
                return Ok(NewPurpose);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Purposes/5
        [HttpPost("{id}")]
        public async Task<IActionResult> DeletePurpose(int id)
        {
            try
            {
                var purpose = await _purposesLogic.DeletePurpose(id);
                if (purpose == false)
                {
                    return Ok("Purpose Deleted Filled");
                }

                return Ok("Purpose Deleted success");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // check if Purpose Exist
        private bool PurposeExists(int id)
        {
            try
            {
                var PurposeExist = _purposesLogic.PurposeExists(id);
                return PurposeExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
