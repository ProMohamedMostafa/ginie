using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GenieMistro.Models;
using GenieMistro.BL;
using Microsoft.AspNetCore.Cors;

namespace GenieMistro.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class MissionsController : ControllerBase
    {
        private readonly genieDBContext _context;
        MissionsLogic _missionsLogic;

        public MissionsController(genieDBContext context)
        {
            _context = context;
            _missionsLogic = new MissionsLogic(_context);
        }

        // GET: api/Missions
        [HttpPost]
        [Route("~/api/Missions/GetMissions")]
        public async Task<ActionResult<IEnumerable<Mission>>> GetMissions()
        {
            try
            {
                var missions = await _missionsLogic.GetMissions();
                return missions;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Missions/5
        [HttpPost("{id}")]
        [Route("~/api/Missions/GetMission/{id}")]
        public async Task<ActionResult<Mission>> GetMission(int id)
        {
            try
            {
                var mission = await _missionsLogic.GetMission(id);

                if (mission == null)
                {
                    return NotFound();
                }

                return mission;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Missions/5
        [HttpPost("{id}")]
        [Route("~/api/Missions/PutMission/{id}")]
        public async Task<IActionResult> PutMission(int id, Mission mission)
        {
            try
            {
                if (id != mission.Id)
                {
                    return BadRequest();
                }
                var comAss = await _missionsLogic.PutMission(id, mission);
                return Ok(mission);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!MissionExists(id))
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

        // POST: api/Missions
        [HttpPost]
        [Route("~/api/Missions/PostMission")]
        public async Task<ActionResult<Mission>> PostMission(Mission mission)
        {
            try
            {
                var NewMission =await _missionsLogic.PostMission(mission);
                return Ok(NewMission);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Missions/5
        [HttpPost("{id}")]
        [Route("~/api/Missions/DeleteMission/{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            try
            {
                var mission = await _missionsLogic.DeleteMission(id);
                if (mission == false)
                {
                    return Ok("Mission Deleted Filled");
                }

                return Ok("Mission Deleted success");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private bool MissionExists(int id)
        {
            try
            {
                var MissionExist = _missionsLogic.MissionExists(id);
                return MissionExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
