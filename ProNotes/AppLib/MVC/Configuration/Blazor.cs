namespace ProNotes.AppLib.MVC.Configuration
{
    public static class Blazor
    {
        public static IServiceCollection _AddServerSideBlazor(this IServiceCollection services)
        {
            services.AddServerSideBlazor(o => o.DetailedErrors = true);

            // Set custom folder for blazor pages (default is Pages)
            // services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Blazor");


            return services;
        }

        public static IApplicationBuilder _UseService(this WebApplication app)
        {
            return app;
        }
    }
}
