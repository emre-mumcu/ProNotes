using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using ProNotes.AppLib.Abstract;
using ProNotes.AppLib.MVC.Requirements;

namespace ProNotes.AppLib.MVC.Configuration
{
    public static class Authorization
    {
        public static IServiceCollection _AddAuthorization<T>(this IServiceCollection services) where T : class, IAuthorize, new()
        {
            services.AddAuthorization(options =>
            {
                /*
                 * Gets or sets the default authorization policy with no policy name specified.
                 * Defaults to require authenticated users.
                 *
                 * The DefaultPolicy is the policy that is applied when:
                 *      (*) You specify that authorization is required, either using RequireAuthorization(), by applying an AuthorizeFilter,
                 *          or by using the[Authorize] attribute on your actions / Razor Pages.
                 *      (*) You don't specify which policy to use.
                 *
                 */

                options.DefaultPolicy = AuthorizationPolicyLibrary.defaultPolicy;

                /*
                 * Gets or sets the fallback authorization policy when no IAuthorizeData have been provided.
                 * By default the fallback policy is null.
                 *
                 * The FallbackPolicy is applied when the following is true:
                 *      (*) The endpoint does not have any authorisation applied. No[Authorize] attribute, no RequireAuthorization, nothing.
                 *      (*) The endpoint does not have an[AllowAnonymous] applied, either explicitly or using conventions.
                 *
                 * So the FallbackPolicy only applies if you don't apply any other sort of authorization policy,
                 * including the DefaultPolicy, When that's true, the FallbackPolicy is used.
                 * By default, the FallbackPolicy is a no - op; it allows all requests without authorization.
                 *
                 */

                options.FallbackPolicy = AuthorizationPolicyLibrary.fallbackPolicy;

                options.InvokeHandlersAfterFailure = false;

                // Custom Policies:
                options.AddPolicy(nameof(AuthorizationPolicyLibrary.userPolicy), AuthorizationPolicyLibrary.userPolicy);
            });

            // Handlers used in Requirements MUST be configured, otherwise authorization  will fail with no visible sign
            services.AddSingleton<IAuthorizationHandler, BaseHandler>();
            services.AddSingleton<IAuthorizationHandler, UserHandler>();

            services.AddSingleton<IAuthorize, T>();

            return services;
        }

        public static IApplicationBuilder _UseAuthorization(this WebApplication app)
        {
            app.UseAuthorization();

            return app;
        }
    }

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
            .AddRequirements(new UserRequirement(new string[] { "User", "Administrator" }))
            .Build();

        public static AuthorizationPolicy adminPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .AddRequirements(new BaseRequirement())
            .AddRequirements(new UserRequirement(new string[] { "Administrator" }))
            // .RequireRole("Administrator")        
            // .RequireAssertion(ctx => { return ctx.User.HasClaim("name1", "val1") || ctx.User.HasClaim("name2", "val2"); })
            .Build();
    }
}

/*
To prevent HandleRequirementAsync method executing twice:
---------------------------------------------------------

Link: https://github.com/dotnet/aspnetcore/issues/32518

Starting in 3.1, authorization attributes are handled by the authorization middleware instead of being evaluated by MVC's authorization filter. 
However if you explicitly add an AuthorizationFilter, it would get separately executed.

Instead of adding a AuthorizationFilter, consider using using RequireAuthorization on an endpoint: e.g.

endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization("some_policy");

This would a substitute to adding a global auth filter. 
 */