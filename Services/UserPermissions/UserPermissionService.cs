using System.Security.Claims;

using EasyAuth.Conf;

namespace EasyAuth.Services.UserPermissions
{
    /// <summary>
    /// Implementation of <see cref="IUserPermissionsService"/> that checks user permissions based on claims.
    /// </summary>
    public sealed class UserPermissionService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPermissionService"/> class.
        /// </summary>
        /// <param name="claims">The claims principal representing the user.</param>
        public UserPermissionService(ClaimsPrincipal claims)
        {
            this.userClaims = claims;

            this.userPermissions = this.userClaims.Claims
                                                  .Where(c => c.Type == Defaults.PermissionClaimType)
                                                  .Select(c => c.Value)
                                                  .ToHashSet();
        }

        private readonly ClaimsPrincipal userClaims;
        private readonly HashSet<string> userPermissions;

        /// <summary>
        /// Checks if the user has a specific permission.
        /// </summary>
        /// <param name="permission">The permission to check.</param>
        /// <returns>True if the user has the specified permission; otherwise, false.</returns>
        public bool HasPermission(string permission)
        {
            return this.userPermissions.Contains(permission);
        }

        /// <summary>
        /// Checks if the user has any of the specified permissions.
        /// </summary>
        /// <param name="permissions">The permissions to check.</param>
        /// <returns>True if the user has at least one of the specified permissions; otherwise, false.</returns>
        public bool HasAnyPermission(params string[] permissions)
        {
            return permissions.Any(p => permissions.Contains(p));
        }
    }
}
