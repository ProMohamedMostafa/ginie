using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenieMistro.Auth;
using GenieMistro.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace GenieMistro.BL
{
    public class auth
    {

        private readonly IConfiguration _configuration;

        public UserManager<ApplicationUser> UserManager { get; set; }
        public auth(UserManager<ApplicationUser> _userManager, IConfiguration _configuration)
        {
            this.UserManager = _userManager;
            this._configuration = _configuration;
        }

        public async Task<String> createUser(RegisterModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName

            };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return   "Done";
            }
            return null;
        }

    }
}
