using EasyAuth.Services.UserPermissions;

using Microsoft.AspNetCore.Authorization;

namespace EasyAuth.Integration.Middleware
{
    /// <summary>
    /// Handles authorization requirements based on user permissions.
    /// </summary>
    public sealed class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// Handles the authorization requirement by checking if the user has the required permission.
        /// </summary>
        /// <param name="context">The authorization context containing the user and resource information.</param>
        /// <param name="requirement">The permission requirement to evaluate.</param>
        /// <returns>A completed task representing the operation.</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userPermissions = new UserPermissionService(context.User);

            if (userPermissions.HasPermission(requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
