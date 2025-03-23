using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace AspCoreStudy.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context
        , PermissionRequirement requirement)
        {
            var permissions = context.User.Claims
                .Where(c => c.Type == "permissions")
                .Select(c => c.Value)
                .ToList();

            if (permissions.Contains(requirement.RequiredPermission))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}