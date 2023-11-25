using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace ProNotes.AppLib.MVC.Defaults
{
    public class CookieAuthenticationOptionsDefaults
    {
        public Action<CookieAuthenticationOptions> GetDefaults()
        {
            Action<CookieAuthenticationOptions> defaults = options =>
            {
                options.Cookie.Name = "";
                options.LoginPath = "";
                options.LogoutPath = "";
                options.AccessDeniedPath = "";
                options.ClaimsIssuer = "";
                options.ReturnUrlParameter = "";
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true; // false: xss vulnerability
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Validate();
                options.Events = new CookieAuthenticationEvents()
                {
                    OnValidatePrincipal = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
            };

            return defaults;
        }
    }
}