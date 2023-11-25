using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ProNotes.AppLib.MVC.Extensions;

namespace ProNotes.AppLib.MVC.Requirements
{
    public class BaseRequirement : IAuthorizationRequirement
    {
        /// IAuthorizationRequirement will contain pure data.
        /// It will have NO services reads, NO dependencies that need to be injectedfor the requirement
        /// The handler will validate the requirement        
    }

    public class BaseHandler : AuthorizationHandler<BaseRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseHandler(IHttpContextAccessor httpContextAccessor) { _httpContextAccessor = httpContextAccessor; }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BaseRequirement requirement)
        {
            if (context is null) throw new ArgumentNullException(nameof(AuthorizationHandlerContext));

            if (context.Resource is HttpContext httpContext)
            {
                var endpoint = httpContext.GetEndpoint();
                var actionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
            }

            try
            {
                bool userSessionValid = _httpContextAccessor?.HttpContext?.Session?.ValidateUserSession() ?? false;

                if (context.User != null && context.User.Identity != null && context.User.Identity.IsAuthenticated && userSessionValid)
                {
                    context?.Succeed(requirement);
                }
                else
                {
                    // context?.Fail();

                    var redirectContext = context.Resource as AuthorizationFilterContext;
                    // redirectContext.Result = new RedirectToActionResult("AccessDenied", "Home", null);

                    redirectContext.Result = new RedirectResult("~/Logout");

                    //context?.Fail();
                    context.Succeed(requirement);
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

/*
    if (context.Resource is HttpContext httpContext)
    {
        var endpoint = httpContext.GetEndpoint();
        var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
    }
    else if (context.Resource is Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext mvcContext)
    {
        // ...
    }
*/