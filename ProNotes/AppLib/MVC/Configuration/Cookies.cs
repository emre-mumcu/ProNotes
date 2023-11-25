using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;

namespace ProNotes.AppLib.MVC.Configuration
{
    public static class Cookies
    {
        public static IServiceCollection _AddCookiePolicy(this IServiceCollection services)
        {
            services
            .Configure<CookiePolicyOptions>(options =>
            {
                // The CheckConsentNeeded option of true will prevent any non-essential cookies 
                // from being sent to the browser (no Set-Cookie header) without the user's explicit permission.
                // You can either change this behaviour, or mark your cookie as essential by setting the 
                // IsEssential property to true when creating it: options.Cookie.IsEssential = true;
                options.CheckConsentNeeded = context => true; // Enable the default cookie consent feature 
                                                              // requires using Microsoft.AspNetCore.Http;
                                                              // options.ConsentCookie.IsEssential = true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.SameAsRequest;

            });

            services.Configure(delegate (CookieTempDataProviderOptions options)
            {
                // GDPR
                options.Cookie.IsEssential = true;
            });

            return services;
        }

        /// <summary>
        /// Cookie Policy Middleware (UseCookiePolicy) conforms the app to the EU General Data Protection Regulation (GDPR) regulations.
        /// </summary>
        public static IApplicationBuilder _UseCookiePolicy(this WebApplication app)
        {
            app.UseCookiePolicy();
            return app;
        }
    }
}
