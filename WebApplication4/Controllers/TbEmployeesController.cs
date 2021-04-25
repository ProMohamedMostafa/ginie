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
    [Route("[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class TbEmployeesController : ControllerBase
    {
        private readonly genieDBContext _context;

        public TbEmployeesController(genieDBContext context)
        {
            _context = context;
        }

        // GET: api/TbEmployees
        [HttpGet]
        [EnableCors("AllowOrigin")]
        [Route("~/api/TbEmployees/GetTbEmployees")]
        public async Task<ActionResult<IEnumerable<TbEmployee>>> GetTbEmployees()
        {
            List<TbEmployee> es2 = new List<TbEmployee>();
            List<TbEmployee> es = new List<TbEmployee>();

            es = await _context.TbEmployees.ToListAsync();
            foreach (TbEmployee t in es )
            {
                TbEmployee temp = new TbEmployee();
                temp.EmpId = t.EmpId;
                temp.EmpName = t.EmpName;
                temp.EmpEmail = t.EmpEmail;
                temp.ManagerId = t.ManagerId;
                es2.Add(temp);
            }
           
            return es2;
        }

        // GET: api/TbEmployees/5
        [HttpGet("{id}")]
        [EnableCors("AllowOrigin")]
        [Route("~/api/TbEmployees/GetTbEmployee")]

        public async Task<ActionResult<TbEmployee>> GetTbEmployee(int id)
        {
            var tbEmployee = await _context.TbEmployees.FindAsync(id);

            if (tbEmployee == null)
            {
                return NotFound();
            }

            return tbEmployee;
        }

        // PUT: api/TbEmployees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [EnableCors("AllowOrigin")]
        [Route("~/api/TbEmployees/PutTbEmployee")]
        public async Task<IActionResult> PutTbEmployee(int id, TbEmployee tbEmployee)
        {
            if (id != tbEmployee.EmpId)
            {
                return BadRequest();
            }

            _context.Entry(tbEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbEmployeeExists(id))
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

        // POST: api/TbEmployees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [EnableCors("AllowOrigin")]
        [Route("~/api/TbEmployees/PostTbEmployee")]
        public async Task<ActionResult<TbEmployee>> PostTbEmployee(TbEmployee tbEmployee)
        {
            _context.TbEmployees.Add(tbEmployee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbEmployee", new { id = tbEmployee.EmpId }, tbEmployee);
        }

        // DELETE: api/TbEmployees/5
        [HttpDelete("{id}")]
        [EnableCors("AllowOrigin")]
        [Route("~/api/TbEmployees/DeleteTbEmployee")]
        public async Task<IActionResult> DeleteTbEmployee(int id)
        {
            var tbEmployee = await _context.TbEmployees.FindAsync(id);
            if (tbEmployee == null)
            {
                return NotFound();
            }

            _context.TbEmployees.Remove(tbEmployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TbEmployeeExists(int id)
        {
            return _context.TbEmployees.Any(e => e.EmpId == id);
        }
    }
}
