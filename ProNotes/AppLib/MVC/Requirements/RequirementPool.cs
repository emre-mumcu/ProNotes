using Microsoft.AspNetCore.Authorization;

namespace ProNotes.AppLib.MVC.Requirements
{
    public class RequirementPool
    {
    }

    public class PermissionHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();

            foreach (var requirement in pendingRequirements)
            {
                // validate each requirement
            }

            return Task.CompletedTask;
        }
    }
}
