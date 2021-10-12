using IdentityCore.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityCore.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "user@outlook.com",
                Email = "user@outlook.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(i => i.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user==null)
                {
                    await userManager.CreateAsync(defaultUser, "$ocL.1972$");
                    await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
                }
            }           
        }
        public static async Task SeedAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "admin@outlook.com",
                Email = "admin@outlook.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(i => i.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "$ocL.1972$");
                    await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Helper.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Asistant.ToString());
                }
                await roleManager.SeedClaimsForAdmin();
            }
        }
        private async static Task SeedClaimsForAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            await roleManager.AddPermissionClaim(adminRole,"Products");
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(i=>i.Type=="Permission"&&i.Value==permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}
