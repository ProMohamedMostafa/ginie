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
    public class CompetenciesController : ControllerBase
    {
        private readonly genieDBContext _context;
        CompetenciesLogic _competenciesLogic;
        public CompetenciesController(genieDBContext context)
        {
            _context = context;
            _competenciesLogic = new CompetenciesLogic(_context);
        }

        // GET: api/Competencies
       [HttpPost]
       [EnableCors("AllowOrigin")]
        [Route("~/api/Competencies/Competencies")]
        public async Task<ActionResult<IEnumerable<Competency>>> Competencies()
        {
            try
            {
                var compAssign = await _competenciesLogic.GetCompetencies();
                return compAssign;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Competencies/5
        [HttpPost("{id}")]
        public async Task<ActionResult<Competency>> GetCompetency(int id)
        {
            try
            {
                var competency = await _competenciesLogic.GetCompetency(id);

                if (competency == null)
                {
                    return NotFound();
                }

                return competency;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Competencies/5
        [Route("~/api/[controller]/[action]/{id}")]
        [HttpPost("{id}")]
        public async Task<ActionResult<Competency>> PutCompetency(int id, Competency competency)
        {
            try
            {
                if (id != competency.Id)
                {
                    return BadRequest();
                }
                var comAss = await _competenciesLogic.PutCompetency(id, competency);
                return Ok(comAss);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CompetencyExists(id))
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

        // POST: api/Competencies
        [HttpPost]
        public async Task<ActionResult<Competency>> PostCompetency(Competency competency)
        {
          try
            {
                var NewCompetency =await _competenciesLogic.PostCompetency(competency);
                return Ok(NewCompetency);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // Delete Competency with id
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteCompetency(int id)
        {
            try
            {
                var compAssign = await _competenciesLogic.DeleteCompetency(id);
                if (compAssign == false)
                {
                    return Ok("Competency Deleted Filled");
                }

                return Ok("Competency Deleted success");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // check if Competency Exist
        private bool CompetencyExists(int id)
        {
            try
            {
                var CompAssignExist = _competenciesLogic.CompetencyExists(id);
                return CompAssignExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
