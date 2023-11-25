using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using mumcu.net.lib.Abstract;
using mumcu.net.tools.Extensions;

namespace mumcu.net.lib.Mvc.Configuration.Ext
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection _AddDefaultAuthentication<T>(this IServiceCollection services) where T : class, IAuthenticate, new()
        {
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddSingleton<IAuthenticate, T>();

            return services;
        }

        public static IServiceCollection _AddAuthentication<T>(this IServiceCollection services) where T: class, IAuthenticate, new()
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.Cookie.Name = AppConstants.Auth_Cookie_Name;
                    options.LoginPath = AppConstants.Auth_Cookie_LoginPath;
                    options.LogoutPath = AppConstants.Auth_Cookie_LogoutPath;
                    options.AccessDeniedPath = AppConstants.Auth_Cookie_AccessDeniedPath;
                    options.ClaimsIssuer = AppConstants.Auth_Cookie_ClaimsIssuer;
                    options.ReturnUrlParameter = AppConstants.Auth_Cookie_ReturnUrl;
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true; // false: xss vulnerability
                    options.ExpireTimeSpan = AppConstants.Auth_Cookie_ExpireTimeSpan;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    options.Validate();
                    options.EventsType = typeof(AppCookieAuthenticationEvents);
                    //options.Events = new CookieAuthenticationEvents()
                    //{
                    //    OnRedirectToLogin = redirectContext =>
                    //    {
                    //        return Task.CompletedTask;
                    //    }
                    //};
                })
            ;

            services.AddScoped<AppCookieAuthenticationEvents>();

            services.AddSingleton<IAuthenticate, T>();

            return services;
        }

        public static IApplicationBuilder _UseAuthentication(this WebApplication app)
        {
            app.UseAuthentication();

            return app;
        }
    }

    public class AppCookieAuthenticationEvents : CookieAuthenticationEvents
    {        
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            bool login = context.HttpContext.Session.GetKey<bool>(AppConstants.SessionKey_Login);

            if (context.Principal != null && context.Principal.Identity != null)
            {
                if (!(context.Principal.Identity.IsAuthenticated && login))
                {
                    context.RejectPrincipal();
                    await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }
            else
            {
                // ???
            }
        }
    }
}