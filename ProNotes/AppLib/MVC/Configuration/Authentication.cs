using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProNotes.AppLib.Abstract;
using System.Reflection;

namespace ProNotes.AppLib.MVC.Configuration
{
    public static class Authentication
    {
        public static IServiceCollection _AddAuthentication<T>(this IServiceCollection services,
            Action<AuthenticationOptions>? authOptions = null,
            Action<CookieAuthenticationOptions>? cookieAuthOptions = null) where T : class, IAuthenticate, new()
        {
            services
                .AddAuthentication(authOptions ?? new AuthenticationOptionsProvider().GetDefaults())
                .AddCookie(cookieAuthOptions ?? new CookieAuthenticationOptionsProvider().GetDefaults());

            services.TryAddScoped<CustomCookieAuthenticationEvents>();

            services.TryAddSingleton<IAuthenticate, T>();

            return services;
        }

        public static IServiceCollection _AddDefaultAuthentication<T>(this IServiceCollection services) where T : class, IAuthenticate, new()
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
            services.TryAddSingleton<IAuthenticate, T>();
            return services;
        }

        public static IApplicationBuilder _UseAuthentication(this WebApplication app)
        {
            app.UseAuthentication();
            return app;
        }
    }

    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        /// <summary>
        /// Once the authentication cookie is created, the cookie is the single source of identity.
        /// The ValidatePrincipal event can be used to intercept and override validation of the cookie identity. 
        /// Validating the authentication cookie on every request mitigates the risk of revoked users accessing the app.
        /// </summary>
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            if (!context.Principal?.Identity?.IsAuthenticated ?? true)
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }

    public class AuthenticationOptionsProvider
    {
        public Action<AuthenticationOptions> GetDefaults()
        {
            return delegate (AuthenticationOptions options)
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            };
        }
    }

    public class CookieAuthenticationOptionsProvider
    {
        public Action<CookieAuthenticationOptions> GetDefaults()
        {
            return delegate (CookieAuthenticationOptions options)
            {
                options.Cookie.Name = $"__Auth-{Assembly.GetExecutingAssembly().ManifestModule.ModuleVersionId}";
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/AccessDenied";
                options.ClaimsIssuer = $"{Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().ManifestModule.Name)}";
                options.ReturnUrlParameter = "ReturnUrl";
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true; // False causes xss vulnerability !!!
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Validate();
                options.EventsType = typeof(CustomCookieAuthenticationEvents);
                options.Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = (context) => Task.CompletedTask
                };
            };
        }
    }
}