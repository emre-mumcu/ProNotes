namespace ProNotes.AppLib.MVC.Configuration
{
    public static class SignalR
    {
        public static IServiceCollection _AddService(this IServiceCollection services)
        {
            services.AddSignalR();
            return services;
        }

        public static IApplicationBuilder _UseService(this WebApplication app)
        {
            return app;
        }
    }
}
