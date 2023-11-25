using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;

namespace ProNotes.AppLib.MVC.Requirements
{
    public class UserRequirement : IAuthorizationRequirement
    {
        public string[] Roles { get; private set; }

        public UserRequirement(params string[] roles)
        {
            Roles = roles;
        }
    }

    public class UserHandler : AuthorizationHandler<UserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            try
            {
                string[] userRoles = context.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();

                if (requirement.Roles.Intersect(userRoles).Any())
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }

                return Task.CompletedTask;
            }
            catch
            {
                throw;
            }
        }
    }
}