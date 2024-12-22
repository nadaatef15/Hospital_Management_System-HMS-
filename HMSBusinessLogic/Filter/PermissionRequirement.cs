using HMSContracts.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HMSBusinessLogic.Filter
{
    public class PermissionRequirement : Attribute , IAuthorizationFilter
    {
        private readonly string permission;
        public PermissionRequirement(string _permission)
        {
            permission= _permission;
        }

        public void OnAuthorization(AuthorizationFilterContext Context)
        {
            var user= Context.HttpContext.User;
            if (user == null)
            {
                Context.Result = new UnauthorizedObjectResult("You need to login");
                return;
            }
            var isAuthenticated = user.Identity?.IsAuthenticated ?? false;

            if (!isAuthenticated)
            {
                Context.Result = new UnauthorizedObjectResult("You are not Authenticated, Login again");
                return;

            }

            var claims = user.Claims.ToList();

            if (!claims.Any(a => a.Type == SysConstants.Permission && a.Value == permission))
            {
                Context.Result = new UnauthorizedObjectResult("You don't have permission");
            }
        }
    }
}
