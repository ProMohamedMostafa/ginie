using GenieMistro.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;


namespace GenieMistro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LandLordController : ControllerBase
    {

        [EnableCors("AllowOrigin")]
        [Route("~/api/LandLord/postLandLord")]
        [HttpPost]

        public ActionResult<landLoard> postLandLord(landLoard landLoard)
        {

            landLoard land = new landLoard();
            try
            { 
            var res=land.CreateDbConnectionString(landLoard);
            if (res != null)
                return Ok(res.Result);
            else
                return Ok(-1);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [EnableCors("AllowOrigin")]
        [Route("~/api/LandLord/CheckCompanyExistAsync")]
        [HttpGet]
        public async Task<ActionResult<landLoard>> CheckCompanyExistAsync(String email)
        {
            Services.landLoard landLoard = new landLoard();
            try
            {

                string id = await landLoard.GetConnectionStringIDAsync(email);
                if (id != null)
                    return Ok(id);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            return StatusCode(StatusCodes.Status500InternalServerError);


        }
    }
}
