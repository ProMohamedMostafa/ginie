using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GenieMistro.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GenieMistro.Services;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Mail;
using GenieMistro.BL;

namespace WebApplication4.Controllers
{
   
   // [Route("api/BusinessAccounts")]
    [Route("[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class BusinessAccountsController : ControllerBase
    {
        private readonly genieDBContext _context;
        BusinessAccountLogic _businessAccount;
       
        public BusinessAccountsController(genieDBContext context)
        {            
            _context = context;
            _businessAccount = new BusinessAccountLogic(_context);
        }


        // GET: api/BusinessAccounts/5
        // [HttpGet("{id}")]
        [HttpPost("{email ,password }")]
        [EnableCors("AllowOrigin")]
        [Route("~/api/BusinessAccounts/GetBusinessAccount")]
        public async Task<ActionResult<BusinessAccount>> GetBusinessAccount(string email, String password)
        {
            try
            {
                var businessAccount = await _businessAccount.GetBusinessAccount(email, password);
                if (businessAccount == null)
                {
                    return NotFound(email + password);
                }
                // String response = "Accepted Username and password you will connected on database:" + Db.Name;
                // return Ok(businessAccount + response);
                return Ok(businessAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        // POST: api/BusinessAccounts
        [EnableCors("AllowOrigin")]
        [Route("~/api/BusinessAccounts/PostBusinessAccount")]
        [HttpPost]
        public async Task<ActionResult<BusinessAccount>> PostBusinessAccount(BusinessAccount businessAccount)
        {
            try
            {
                if (_context.BusinessAccounts.Any(e => e.Email == businessAccount.Email))
                    return StatusCode(500, "Email is already exist");
                if (_context.BusinessAccounts.Any(e => e.BaWebSite == businessAccount.BaWebSite))
                    return StatusCode(500, " WebSite is already exist");
                if (_context.BusinessAccounts.Any(e => e.BPhone == businessAccount.BPhone))
                    return StatusCode(500, " Phone is already exist");
                if (_context.BusinessAccounts.Any(e => e.CompanyName == businessAccount.CompanyName))
                    return StatusCode(500, " Company Name is already exist");


                var Account = await _businessAccount.PostBusinessAccount(businessAccount);
                return Ok(Account);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [EnableCors("AllowOrigin")]
        [Route("~/api/BusinessAccounts/postLandLord")]
        [HttpPost]
        public ActionResult<landLoard> postLandLord(landLoard landLoard)
        {
            try
            {
                var BusinessAccount = _businessAccount.postLandLord(landLoard);
                return Ok(BusinessAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }


        // PUT: api/BusinessAccounts/5
        [EnableCors("AllowOrigin")]
        [Route("~/api/BusinessAccounts/PutBusinessAccount/{id}")]
         [HttpPost("{id}")]
        public async Task<IActionResult> PutBusinessAccount(int id, BusinessAccount businessAccount)
        {
           
            try
            {
                if (id != businessAccount.Id)
                {
                    return BadRequest();
                }
                var update = await _businessAccount.PutBusinessAccount(id, businessAccount);
                return Ok(update);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BusinessAccountExists(id))
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

        // GET: api/BusinessAccounts
        [EnableCors("AllowOrigin")]
        [Route("~/api/BusinessAccounts/Get")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<BusinessAccount>>> Get()
        {
            try
            {
                var Accounts = await _businessAccount.Get();
                return Accounts;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/BusinessAccounts/5
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteBusinessAccount(int id)
        {
            try
            {
                var businessAccount = await _businessAccount.DeleteBusinessAccount(id);
                if (businessAccount == false)
                {
                    return Ok("Account Deleted Filled");
                }

                return Ok("Account Deleted success");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // check if account Exist
        private bool BusinessAccountExists(int id)
        {
            try
            {
                var AccountExist = _businessAccount.BusinessAccountExists(id);
                return AccountExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }





    }
}
