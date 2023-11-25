using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ProNotes.AppLib.MVC.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Comma-seperated list of allowed role names
        /// </summary>
        public string AllowedRoles { get; set; } = null!;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Do not engage if AllowAnonymousAttribute exists
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any()) return;

            if (string.IsNullOrEmpty(AllowedRoles))
            {
                // context.Result = new UnauthorizedResult();
                // return;
                throw new Exception("Allowed roles can not be empty.");
            }

            IEnumerable<string> AllowedRolesList = AllowedRoles.Split(',').Select(i => i.Trim()).ToList();

            ClaimsPrincipal user = context.HttpContext.User;

            foreach (string RoleName in AllowedRolesList)
            {
                if (user.IsInRole(RoleName)) return;
            }

            // If NO roles matches:
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}