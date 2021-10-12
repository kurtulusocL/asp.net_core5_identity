using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCore.Permission
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler()
        {

        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }
            var permissions = context.User.Claims.Where(i => i.Type == "Permission" && i.Value == requirement.Permission && i.Issuer == "LOCAL AUTHORITY");
            if (permissions.Any())
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
