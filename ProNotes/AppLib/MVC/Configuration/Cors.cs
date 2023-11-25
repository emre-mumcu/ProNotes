using Microsoft.Net.Http.Headers;

namespace ProNotes.AppLib.MVC.Configuration
{
    // https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0

    public static class Cors
    {
        private static string LocalOrigins = "AppLocalOrigins";

        public static IServiceCollection _AddCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: LocalOrigins, policy =>
                {
                    policy
                        // .AllowAnyOrigin() // Allows CORS requests from all origins with any scheme (http or https). AllowAnyOrigin is insecure because any website can make cross-origin requests to the app.
                        .WithOrigins("http://localhost:44388")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithMethods("GET", "POST")
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                    // .WithHeaders(HeaderNames.ContentType, "x-custom-header")
                    // .WithExposedHeaders("x-custom-header")
                    ;
                });
            });

            return services;
        }

        /// <summary>
        /// When using Response Caching Middleware, call UseCors before UseResponseCaching
        /// UseStaticFiles is called before UseCors. Apps that use JavaScript to retrieve static files cross site must call UseCors before UseStaticFiles.
        /// With endpoint routing, CORS can be enabled on a per-endpoint basis using the RequireCors set of extension methods:
        /// endpoints.MapControllers().RequireCors(LocalOrigins);
        /// </summary>        
        public static IApplicationBuilder _UseCors(this WebApplication app)
        {
            app.UseCors(LocalOrigins);

            return app;
        }
    }
}
