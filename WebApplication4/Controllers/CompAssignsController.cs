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
    [EnableCors("AllowOrigin")]
    [Route("~/api/[controller]/[action]")]
    [ApiController]
    public class CompAssignsController : ControllerBase
    {
        private readonly genieDBContext _context;
        CompAssignsLogic _compAssignsLogic;
        public CompAssignsController(genieDBContext context)
        {
          
            _context = context;
            _compAssignsLogic = new CompAssignsLogic(_context);
        }

        // GET All CompAssigns
        [HttpPost]
        [Route("~/api/CompAssigns/GetCompAssigns")]
        public async Task<ActionResult<IEnumerable<CompAssign>>> GetCompAssigns()
        {
            try
            {
                var compAssign = await _compAssignsLogic.GetCompAssigns();
                return compAssign;

           
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET CompAssign with id
        [HttpPost("{id}")]
        [Route("~/api/CompAssigns/GetCompAssign/{id}")]
        public async Task<ActionResult<CompAssign>> GetCompAssign(int id)
        {
            try
            {
                var compAssign = await _compAssignsLogic.GetCompAssign(id);

                if (compAssign == null)
                {
                    return NotFound();
                }

                return compAssign;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }

        // PUT CompAssigns with id
        [HttpPost("{id}")]
        [Route("~/api/CompAssigns/PutCompAssign/{id}")]
        public async Task<IActionResult> PutCompAssign(int id, CompAssign compAssign)
        {
            try
            {
                if (id != compAssign.Id)
                {
                    return BadRequest();
                }
                var comAss = await _compAssignsLogic.PutCompAssign(id, compAssign);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CompAssignExists(id))
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

        // Post CompAssign
        [EnableCors("AllowOrigin")]
        [Route("~/api/CompAssigns/PostCompAssign")]
        [HttpPost]
        public async Task<ActionResult<CompAssign>> PostCompAssign(CompAssign compAssign)
        {
            try
            {
                var NewCompAssign = _compAssignsLogic.PostCompAssign(compAssign);
                return Ok(NewCompAssign);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
       
        }

        // DELETE CompAssigns 
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteCompAssign(int id)
        {
            try
            {
                var compAssign = await _compAssignsLogic.DeleteCompAssign(id);
                if (compAssign == false)
                {
                    return Ok("compAssign Deleted Filled");
                }

                return Ok("compAssign Deleted success");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

      
        // check if CompAssign Exist
        private bool CompAssignExists(int id)
        {
            try
            {
                var CompAssignExist = _compAssignsLogic.CompAssignExists(id);
                return CompAssignExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
