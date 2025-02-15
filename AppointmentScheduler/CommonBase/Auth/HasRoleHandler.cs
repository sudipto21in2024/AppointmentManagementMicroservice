using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Auth
{
    public class HasRoleHandler : AuthorizationHandler<HasRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasRoleRequirement requirement)
        {
            if (context.User.IsInRole(requirement.Role))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
