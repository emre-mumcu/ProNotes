using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using ProNotes.AppLib.MVC.Requirements;

namespace ProNotes.AppLib.MVC.Defaults
{
    public static class AuthorizationPolicyLibrary
    {
        public static AuthorizationPolicy defaultPolicy = new AuthorizationPolicyBuilder()
           .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
           .RequireAuthenticatedUser()
           .Build();

        public static AuthorizationPolicy fallbackPolicy = new AuthorizationPolicyBuilder()
           .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
           .RequireAuthenticatedUser()
           .Build();

        public static AuthorizationPolicy userPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .AddRequirements(new BaseRequirement())
            //.RequireRole("User")            
            .Build();

        public static AuthorizationPolicy adminPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .AddRequirements(new BaseRequirement())
            .AddRequirements(new UserRequirement("Administrator"))
            //.RequireRole("Administrator")
            // The RequireAssertion method takes a lambda that receives the HttpContext object and returns a Boolean value.
            // .RequireAssertion(ctx => { return ctx.User.HasClaim("name1", "val1") || ctx.User.HasClaim("name2", "val2"); })
            .Build();
    }
}
