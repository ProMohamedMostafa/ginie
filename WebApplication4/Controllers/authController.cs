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



        private  IConfiguration configuration { get; set; }

        public UserManager<ApplicationUser> UserManager { get; set; }
        private RoleManager<IdentityRole> roleManager { get; set; }
       private SignInManager<ApplicationUser> signInManager;

        public authController(UserManager<ApplicationUser> _userManager, IConfiguration _configuration, RoleManager<IdentityRole> _roleManager, SignInManager<ApplicationUser> _signInManager)
        {
            auth = new auth(_userManager, _configuration,_roleManager);
            this.signInManager = _signInManager;
            this.UserManager = _userManager;
            this.configuration = _configuration;
            this.roleManager = _roleManager;

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
                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                var token = new JwtSecurityToken
                    (
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:validAudience"],
                    expires: DateTime.Now.AddHours(10000),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                    );
                /*var res=  await signInManager.PasswordSignInAsync(model.UserName,
                             model.Password,false, lockoutOnFailure: true);*/
                return Ok(new
                {

                    token = new JwtSecurityTokenHandler().WriteToken(token)
                    /* Expiration = token.ValidTo,
                     User = user.UserName*/


                }) ;


            }

            return Unauthorized();


        }
        [HttpPost]

        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExist = await UserManager.FindByNameAsync(model.UserName);
            if (userExist != null)
                return StatusCode(StatusCodes.Status409Conflict);
            String rm = await auth.createUser(model);

            if (rm != null)
                return Ok(rm);

            return StatusCode(StatusCodes.Status500InternalServerError);

        }
        [HttpPost]
        [Route("~/api/auth/RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExist = await UserManager.FindByNameAsync(model.UserName);
            if (userExist != null)
                return StatusCode(StatusCodes.Status409Conflict);
            String rm = await auth.CreateAdmin(model);

            if (rm != null)
                return Ok(rm);

            return StatusCode(StatusCodes.Status500InternalServerError);

        }

    }
}
