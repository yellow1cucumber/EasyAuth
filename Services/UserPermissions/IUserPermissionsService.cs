namespace EasyAuth.Services.UserPermissions
{
    /// <summary>
    /// Provides methods to check user permissions.
    /// </summary>
    public interface IUserPermissionsService
    {
        /// <summary>
        /// Checks if the user has a specific permission.
        /// </summary>
        /// <param name="permission">The permission to check.</param>
        /// <returns>True if the user has the specified permission; otherwise, false.</returns>
        bool HasPermission(string permission);

        /// <summary>
        /// Checks if the user has any of the specified permissions.
        /// </summary>
        /// <param name="permissions">The permissions to check.</param>
        /// <returns>True if the user has at least one of the specified permissions; otherwise, false.</returns>
        bool HasAnyPermission(params string[] permissions);
    }
}
