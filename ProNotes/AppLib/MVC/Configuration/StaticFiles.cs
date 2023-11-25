using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace ProNotes.AppLib.MVC.Configuration
{
    public static class StaticFiles
    {
        /// <summary>
        /// Static File Middleware (UseStaticFiles) returns static files and short-circuits further request processing.
        /// UseStaticFiles is called before UseCors. Apps that use JavaScript to retrieve static files cross site must call UseCors before UseStaticFiles
        /// </summary>
        public static IApplicationBuilder _UseStaticFiles(this WebApplication app, string? root = null, string? requestPath = null)
        {
            if (root == null && requestPath == null)
            {
                app.UseStaticFiles();
            }
            else
            {
                StaticFileOptions staticFileOptions = new StaticFileOptions();
                if (root != null)
                {
                    staticFileOptions.FileProvider = new PhysicalFileProvider(root);
                }

                if (requestPath != null)
                {
                    staticFileOptions.RequestPath = requestPath;
                }

                app.UseStaticFiles(staticFileOptions);
            }

            return app;
        }
    }
}