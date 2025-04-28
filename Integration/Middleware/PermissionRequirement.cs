using Microsoft.AspNetCore.Authorization;

namespace EasyAuth.Integration.Middleware
{
    public sealed class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionRequirement"/> class.
        /// </summary>
        /// <param name="permission">The permission required.</param>
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }

        /// <summary>
        /// Gets the permission required.
        /// </summary>
        public string Permission { get; }
    }
}
