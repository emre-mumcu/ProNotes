using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ProNotes.AppLib.MVC.Attributes
{
    // https://code-maze.com/action-filters-aspnetcore/

    /// <summary>
    /// This action filter checks if there is a specified custom header attribute with a specified value in the user request
    /// If not, action execution is prevented
    /// </summary>
    public class RequireHeaderActionFilter : IActionFilter
    {
        public string HeaderKey { get; }
        public string HeaderValue { get; }

        public RequireHeaderActionFilter(string headerKey, string headerValue)
        {
            HeaderKey = headerKey;
            HeaderValue = headerValue;
        }

        // Before action execute
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string headerSentByClient = context.HttpContext.Request.Headers[HeaderKey];

            if (headerSentByClient == HeaderValue)
            {
                // OK
            }
            else
            {
                // Invalid request
                // Setting the result with a non-null value inside an action filter will short-circuit the action and any remaining action filters
                //context.Result = new ForbidResult();
                throw new HttpRequestException("header", null, HttpStatusCode.Unauthorized);
            }
        }

        // After action execute
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

    public class RequireHeaderAttribute : TypeFilterAttribute
    {
        public RequireHeaderAttribute(string headerKey, string headerValue) : base(typeof(RequireHeaderActionFilter))
        {
            Arguments = new object[] { headerKey, headerValue };
        }
    }
}

/*
Like the other types of filters, the action filter can be added to different scope levels: Global, Action, Controller.

If we want to use our filter globally, we need to register it inside the AddControllers() method in the ConfigureServices method:

public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers(config => 
    {
        config.Filters.Add(new GlobalFilterExample());
    });
}

But if we want to use our filter as a service type on the Action or Controller level, we need to register it in the same ConfigureServices method but as a service in the IoC container:

services.AddScoped<ActionFilterExample>();
services.AddScoped<ControllerFilterExample>();

Finally, to use a filter registered on the Action or Controller level, we need to place it on top of the Controller or Action as a ServiceType:

[ServiceFilter(typeof(ControllerFilterExample))]
[ServiceFilter(typeof(ActionFilterExample), Order=2)]
[ServiceFilter(typeof(ActionFilterExample2), Order=1)]
*/
