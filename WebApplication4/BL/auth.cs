using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenieMistro.Auth;
using GenieMistro.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using GenieMistro.DTO;

namespace GenieMistro.BL
{
    public class auth
    {

        private readonly IConfiguration _configuration;

        public UserManager<ApplicationUser> UserManager { get; set; }
        private RoleManager<IdentityRole> roleManager { get; set; }
        public auth(UserManager<ApplicationUser> _userManager, IConfiguration _configuration, RoleManager<IdentityRole> _roleManager)
        {
            this.UserManager = _userManager;
            this._configuration = _configuration;
            this.roleManager = _roleManager;
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
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await UserManager.AddToRoleAsync(user, UserRoles.User);
                }
                return   "Done";
            }
            return null;
        }

        public async Task<String> CreateAdmin(RegisterModel model)
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
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await UserManager.AddToRoleAsync(user, UserRoles.Admin);
                }
                return "Done";
            }

            return null;
        }

    }
}
