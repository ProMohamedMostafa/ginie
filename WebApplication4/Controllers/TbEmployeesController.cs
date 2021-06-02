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
    [Route("[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class TbEmployeesController : ControllerBase
    {
        private readonly genieDBContext _context;
        TbEmployeesLogic _tbEmployeesLogic;

        public TbEmployeesController(genieDBContext context)
        {
            _context = context;
            _tbEmployeesLogic = new TbEmployeesLogic(_context);
        }

        // GET: api/TbEmployees
        //[HttpGet]
        [HttpPost]
        [EnableCors("AllowOrigin")]
        [Route("~/api/TbEmployees/GetTbEmployees")]
        public async Task<ActionResult<IEnumerable<TbEmployee>>> GetTbEmployees()
        {
            try
            {
                var tbEmployees = await _tbEmployeesLogic.GetTbEmployees();
                return tbEmployees;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/TbEmployees/5
        [EnableCors("AllowOrigin")]
        [Route("~/api/TbEmployees/GetTbEmployee/{id}")]
        [HttpPost("{id}")]
        public async Task<ActionResult<TbEmployee>> GetTbEmployee(int id)
        {
            try
            {
                var tbEmployee = await _tbEmployeesLogic.GetTbEmployee(id);

                if (tbEmployee == null)
                {
                    return NotFound();
                }

                return tbEmployee;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT: api/TbEmployees/5
        [EnableCors("AllowOrigin")]
        [Route("~/api/TbEmployees/PutTbEmployee/{id}")]
        [HttpPost("{id}")]
        public async Task<ActionResult<TbEmployee>> PutTbEmployee(int id, TbEmployee tbEmployee)
        {
            try
            {
                if (id != tbEmployee.Id)
                {
                    return BadRequest();
                }
                var comAss = await _tbEmployeesLogic.PutTbEmployee(id, tbEmployee);
                return Ok(tbEmployee);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TbEmployeeExists(id))
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

        // POST: api/TbEmployees
        [HttpPost]
        [EnableCors("AllowOrigin")]
        [Route("~/api/TbEmployees/PostTbEmployee")]
        public async Task<ActionResult<TbEmployee>> PostTbEmployee(TbEmployee tbEmployee)
        {
            try
            {
                var NewTbEmployee = await _tbEmployeesLogic.PostTbEmployee(tbEmployee);
                return Ok(NewTbEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/TbEmployees/5
        [HttpPost("{id}")]
        [EnableCors("AllowOrigin")]
        [Route("~/api/TbEmployees/DeleteTbEmployee/{id}")]
        public async Task<IActionResult> DeleteTbEmployee(int id)
        {
            try
            {
                var Employee = await _tbEmployeesLogic.DeleteTbEmployee(id);
                if (Employee == false)
                {
                    return Ok("Employee Deleted Filled");
                }

                return Ok("Employee Deleted success");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



       
        [EnableCors("AllowOrigin")]
        [Route("~/api/TbEmployees/GetLevelsCount")]
       [HttpPost]
        public  int GetLevelsCount()
        {
            try
            {
                var max = _tbEmployeesLogic.GetLevelsCount();
                return max;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // check if TbEmployees Exist
        private bool TbEmployeeExists(int id)
        {
            try
            {
                var tbEmployeesExist = _tbEmployeesLogic.TbEmployeeExists(id);
                return tbEmployeesExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
