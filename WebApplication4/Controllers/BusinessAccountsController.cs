using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenieMistro.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebApplication4.Controllers
{
   
   // [Route("api/BusinessAccounts")]
    [Route("[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class BusinessAccountsController : ControllerBase
    {
        private readonly genieDBContext _context;

        public BusinessAccountsController(genieDBContext context)
        {
            _context = context;
        }


        // GET: api/BusinessAccounts/5
        [HttpGet("{id}")]
        [EnableCors("AllowOrigin")]
        [Route("~/api/BusinessAccounts/GetBusinessAccount")]

        public ActionResult<BusinessAccount> GetBusinessAccount(string email, String password)
        {
            List<BusinessAccount> bs = _context.BusinessAccounts.ToList();
            BusinessAccount businessAccount = new BusinessAccount();
            foreach (BusinessAccount b in bs)
            {
                if (b.Email.Trim() == email.Trim() && b.BaPassword.Trim() == password.Trim())
                    businessAccount = b;
                break;
            }
            if (businessAccount == null)
            {
                return NotFound(email + password);
            }
            return businessAccount;


        }

        // PUT: api/BusinessAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors("AllowOrigin")]
        [Route("~/api/BusinessAccounts/PutBusinessAccount")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessAccount(int id, BusinessAccount businessAccount)
        {
            if (id != businessAccount.Id)
            {
                return BadRequest();
            }

            _context.Entry(businessAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessAccountExists(id))
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

        // POST: api/BusinessAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [EnableCors("AllowOrigin")]
        [Route("~/api/BusinessAccounts/PostBusinessAccount")]
        [HttpPost]
        
        public async Task<ActionResult<BusinessAccount>> PostBusinessAccount(BusinessAccount businessAccount)
        {
          
            
            
           if( _context.BusinessAccounts.Any(e => e.Email == businessAccount.Email))
                return StatusCode(500, "Email is already exist");
            if (_context.BusinessAccounts.Any(e => e.BaWebSite == businessAccount.BaWebSite))
                return StatusCode(500, " WebSite is already exist");
            if (_context.BusinessAccounts.Any(e => e.BPhone == businessAccount.BPhone))
                return StatusCode(500, " Phone is already exist");
            if (_context.BusinessAccounts.Any(e => e.CompanyName == businessAccount.CompanyName))
                return StatusCode(500, " Company Name is already exist");
          



            _context.BusinessAccounts.Add(businessAccount);
            await _context.SaveChangesAsync();
            

           // return CreatedAtAction("GetBusinessAccount", new { id = businessAccount.Id }, businessAccount);
            return Ok(businessAccount);
        }


        // GET: api/BusinessAccounts
        [EnableCors("AllowOrigin")]
        [Route("~/api/BusinessAccounts/Get")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessAccount>>> Get()
        {
            return await _context.BusinessAccounts.ToListAsync();
        }
       // DELETE: api/BusinessAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessAccount(int id)
        {
            var businessAccount = await _context.BusinessAccounts.FindAsync(id);
            if (businessAccount == null)
            {
                return NotFound();
            }

            _context.BusinessAccounts.Remove(businessAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BusinessAccountExists(int id)
        {
            return _context.BusinessAccounts.Any(e => e.Id == id);
        }
    }
}
