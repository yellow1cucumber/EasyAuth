using Microsoft.AspNetCore.Authorization;

namespace EasyAuth.Integration.Attributes
{
    /// <summary>  
    /// Represents an attribute that specifies a policy-based authorization requirement  
    /// based on a specific permission.  
    /// </summary>  
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class AuthorizeByPermissionAttribute : AuthorizeAttribute
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="AuthorizeByPermissionAttribute"/> class.  
        /// </summary>  
        /// <param name="permission">The permission required for authorization.</param>  
        /// <remarks>  
        /// The <paramref name="permission"/> parameter is used to set the policy name  
        /// that will be checked during the authorization process.  
        /// </remarks>  
        public AuthorizeByPermissionAttribute(string permission)
        {
            Policy = permission;
        }
    }
}
