using IdentityCore.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCore.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Asistant.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Helper.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        }
    }
}
