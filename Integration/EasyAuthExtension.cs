using EasyAuth.Integration.Middleware;
using EasyAuth.Services.UserPermissions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAuth.Integration
{
    /// <summary>
    /// Provides extension methods for integrating EasyAuth into an application.
    /// </summary>
    public static class EasyAuthExtension
    {
        /// <summary>
        /// Adds EasyAuth services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddEasyAuth(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddTransient<IUserPermissionsService, UserPermissionService>();

            return services;
        }

        /// <summary>
        /// Adds a permission-based authorization policy to the specified <see cref="AuthorizationOptions"/>.
        /// </summary>
        /// <param name="options">The <see cref="AuthorizationOptions"/> to configure.</param>
        /// <param name="permission">The name of the permission required by the policy.</param>
        /// <returns>The updated <see cref="AuthorizationOptions"/>.</returns>
        public static AuthorizationOptions AddPermissionPolicy(this AuthorizationOptions options, string permission)
        {
            options.AddPolicy(permission, policy =>
            {
                policy.Requirements.Add(new PermissionRequirement(permission));
            });
            return options;
        }
    }
}
