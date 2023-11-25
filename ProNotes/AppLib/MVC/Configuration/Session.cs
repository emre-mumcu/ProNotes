using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Reflection;

namespace ProNotes.AppLib.MVC.Configuration
{
    public static class Session
    {
        public static IServiceCollection _AddSession(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = "session__cookie";
            //    options.IdleTimeout = TimeSpan.FromMinutes(20);
            //    // for security reasons
            //    options.Cookie.HttpOnly = true;
            //    // Session state cookies are not essential by default. 
            //    // Session state isn't functional when tracking is disabled bu user (GDPR). 
            //    // Make the session cookie Essential:
            //    options.Cookie.IsEssential = true;
            //    options.Cookie.SameSite = SameSiteMode.Lax;
            //});

            services.AddSession(delegate (SessionOptions options)
            {
                options.Cookie.Name = $"__Session-{Assembly.GetExecutingAssembly().ManifestModule.ModuleVersionId}";
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true; // security
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
            });

            return services;
        }

        /// <summary>
        /// Call Session Middleware after Cookie Policy Middleware and 
        /// before MVC Middleware (app.MapRazorPages) or Endpoint Routing Middleware (app.UseEndpoints)
        /// </summary>
        public static IApplicationBuilder _UseSession(this WebApplication app)
        {
            app.UseSession();
            return app;
        }
    }
}