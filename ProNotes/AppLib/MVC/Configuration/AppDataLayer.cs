using ProNotes.AppData.EFCore.Context;

namespace ProNotes.AppLib.MVC.Configuration
{
    public static class AppDataLayer
    {
        public static IServiceCollection _AddDataLayer(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            return services;
        }

        //public static async Task<IApplicationBuilder> _InitDataLayer(this WebApplication app)
        //{
        //    await DataGenerator.GenerateAsync(app.Services);
        //    return app;
        //}
    }
}
