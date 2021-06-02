using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenieMistro.DTO;
using GenieMistro.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Cors;
using GenieMistro.BL;

namespace GenieMistro.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class authController : ControllerBase
    {
        BL.auth auth;



        private readonly IConfiguration _configuration;

        public UserManager<ApplicationUser> UserManager { get; set; }

        public authController(UserManager<ApplicationUser> _userManager, IConfiguration _configuration)
        {
            auth = new auth(_userManager, _configuration);

            this.UserManager = _userManager;
            this._configuration = _configuration;
        }

        [HttpPost]

        public async Task<IActionResult> login([FromBody] LoginModel model)
        {
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user != null && await UserManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await UserManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                foreach (var userRols in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRols));

                }
                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken
                    (
                    issuer: _configuration["JWT:ValidIsUser"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(10),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {

                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    User = user.UserName


                });


            }

            return Unauthorized();


        }
        [HttpPost]

        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            String rm = await auth.createUser(model);

            if (rm != null)
                return Ok(rm);

            return StatusCode(StatusCodes.Status500InternalServerError);

        }

    }
}
