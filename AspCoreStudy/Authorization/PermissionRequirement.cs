using Microsoft.AspNetCore.Authorization;

namespace AspCoreStudy.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string RequiredPermission { get; }

        public PermissionRequirement(string requiredPermission)
        {
            RequiredPermission = requiredPermission;
        }
    }
}