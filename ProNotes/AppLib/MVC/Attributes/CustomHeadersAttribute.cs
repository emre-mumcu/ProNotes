using Microsoft.AspNetCore.Mvc.Filters;

namespace ProNotes.AppLib.MVC.Attributes
{
    // https://en.wikipedia.org/wiki/List_of_HTTP_header_fields
    public class CustomHeadersAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.HttpContext.Response.Headers.TryGetValue("Developer", out _))
                context.HttpContext.Response.Headers.Add("Developer", "mumcusoft");

            // https://web.dev/csp/#if_you_absolutely_must_use_it
            if (!context.HttpContext.Response.Headers.TryGetValue("Content-Security-Policy", out _))
                context.HttpContext.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");

            if (!context.HttpContext.Response.Headers.TryGetValue("X-Frame-Options", out _))
                context.HttpContext.Response.Headers.Add("X-Frame-Options", "sameorigin");

            base.OnResultExecuting(context);
        }
    }
}
