namespace ProNotes.AppLib.MVC.Configuration
{
    public static class ResponseCaching
    {
        public static IServiceCollection _AddResponseCaching(this IServiceCollection services)
        {
            services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024;
                options.UseCaseSensitivePaths = true;
            });

            return services;
        }

        /// <summary>
        /// UseCors must be called before UseResponseCaching
        /// </summary>
        public static IApplicationBuilder _UseResponseCaching(this WebApplication app)
        {
            app.UseResponseCaching();

            return app;
        }
    }
}
