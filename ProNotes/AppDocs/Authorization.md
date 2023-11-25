using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using mumcu.net.lib.Abstract;
using mumcu.net.lib.Concrete;
using mumcu.net.lib.Mvc.Requirements;

namespace mumcu.net.lib.Mvc.Configuration.Ext
{
    public static class Authorization
    {
        public static IServiceCollection _AddAuthorization(this IServiceCollection services)
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

                //options.DefaultPolicy = new AuthorizationPolicyBuilder()
                //     .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                //     .RequireAuthenticatedUser()
                //     .AddRequirements(new BaseRequirement())
                //     .Build();

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

                //options.FallbackPolicy = new AuthorizationPolicyBuilder()
                //     .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                //     .RequireAuthenticatedUser()
                //     .AddRequirements(new BaseRequirement())
                //     .Build();

                options.FallbackPolicy = AuthorizationPolicyLibrary.fallbackPolicy;

                // Custom Policies:

                // options.AddPolicy(AdminRequirement.PolicyName, AuthorizationPolicyLibrary.adminPolicy);
                // options.AddPolicy(UserRequirement.PolicyName, AuthorizationPolicyLibrary.userPolicy);
            });


            /// Handlers for Requirements used in policies MUST be  added to services, otherwise authorization fails sometimes with no error but access is always denied.
            /// Be carefulll!!!!

            services.AddSingleton<IAuthorizationHandler, BaseHandler>();
            services.AddSingleton<IAuthorizationHandler, UserHandler>();

            // TODO : Authorizer
            services.AddSingleton<IAuthorize, TestAuthorize>();

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
           .AddRequirements(new BaseRequirement())
           .Build();

        public static AuthorizationPolicy fallbackPolicy = new AuthorizationPolicyBuilder()
           .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
           .RequireAuthenticatedUser()
           .AddRequirements(new BaseRequirement())
           .Build();

        public static AuthorizationPolicy userPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .AddRequirements(new BaseRequirement())
            .AddRequirements(new UserRequirement("User"))
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

    // https://github.com/aspnet/Security/blob/master/src/Microsoft.AspNetCore.Authorization.Policy/PolicyEvaluator.cs
    // https://github.com/dotnet/aspnetcore/blob/main/src/Shared/SecurityHelper/SecurityHelper.cs
    // https://github.com/dotnet/aspnetcore/tree/main/src    
}