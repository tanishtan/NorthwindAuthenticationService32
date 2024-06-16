using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using NorthwindModelClassLibrary;

namespace ProductsAPI.Infrastructure
{
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public string? _rolesString;
        private string[] allowedRoles;

        public CustomAuthorizationAttribute(string? roles)
        {
            allowedRoles = new string[1];
            if (!string.IsNullOrEmpty(roles))
            {
                _rolesString = roles;
                allowedRoles = roles.Split(',');
            }

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as UserModel;
            if (user is not null)
            {
                var role = user.RoleName;
                if (!allowedRoles.ToList().Exists(c => c == role))
                {
                    context.Result = new JsonResult(
                    new { message = "You are not authorized to use this APIs. Contact your admin." }
                    )
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
            }
            else
            {
                context.Result = new JsonResult(
                    new { message = "You are not authorized to use this APIs. Contact your admin." }
                    )
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
