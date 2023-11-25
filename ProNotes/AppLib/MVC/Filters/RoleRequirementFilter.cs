using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ProNotes.AppLib.MVC.Filters
{
    public class RoleRequirementFilter : IAuthorizationFilter
    {
        private readonly string _role;

        public RoleRequirementFilter(string Role)
        {
            _role = Role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user != null)
            {

                if (user.Identity == null || !user.Identity.IsAuthenticated)
                {
                    // it isn't needed to set unauthorized result 
                    // as the base class already requires the user to be authenticated
                    // this also makes redirect to a login page work properly
                    // context.Result = new UnauthorizedResult();
                    return;
                }

                bool userHasRole = false;

                IEnumerable<Claim> roleClaims = context.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role);

                foreach (Claim c in roleClaims)
                {
                    if (c.Value.Contains(_role)) userHasRole = true;
                }

                if (!userHasRole)
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
