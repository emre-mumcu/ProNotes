using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProNotes.AppLib.MVC.Configuration
{
    public static class Options
    {
        public static IServiceCollection _AddOptions<T>(this IServiceCollection services, string path) where T : class, new()
        {
            services.AddOptions();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile(path, optional: true, reloadOnChange: true).Build();
            services.Configure<T>(config);

            return services;
        }

        public static IApplicationBuilder _UseOptions(this WebApplication app)
        {
            return app;
        }
    }
}