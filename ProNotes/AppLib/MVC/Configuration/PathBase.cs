namespace ProNotes.AppLib.MVC.Configuration
{
    public static class PathBase
    {
        //public static IServiceCollection _AddPathBase(this IServiceCollection services)
        //{
        //    return services;
        //}

        public static IApplicationBuilder _UsePathBase(this WebApplication app)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == app.Environment.ApplicationName)
            {
                // If ProcessName is equal to ApplicationName, then application is running in Kestrel (not iisexpress)
                // In Kestrel, you need to set PathBase in code, else you will get: "A path base can only be configured using IApplicationBuilder.UsePathBase" error if you configure builder manually
                app.UsePathBase("/AspNet6.Web");
            }

            return app;
        }
    }
}
