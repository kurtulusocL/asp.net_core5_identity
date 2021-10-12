using IdentityCore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityCore.Helpers
{
    public static class ClaimsHelper
    {
        public static void GetPermissions(this List<RoleClaimsViewModel> allPermissions, Type policy, string roleId)
        {
            FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo item in fields)
            {
                allPermissions.Add(new RoleClaimsViewModel { Value = item.GetValue(null).ToString(), Type = "Permissions" });
            }
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(i => i.Type == "Permission" && i.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
