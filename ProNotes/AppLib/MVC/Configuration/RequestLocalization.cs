using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;

namespace ProNotes.AppLib.MVC.Configuration
{
    public static class RequestLocalizationOptionsDefaults
    {
        public static Action<RequestLocalizationOptions> GetDefaults()
        {
            CultureInfo trCulture = new CultureInfo("tr-TR");
            CultureInfo enCulture = new CultureInfo("en-US");

            return delegate (RequestLocalizationOptions options)
            {
                options.DefaultRequestCulture = new RequestCulture(trCulture);
                options.SupportedCultures = new[] { trCulture, enCulture };
                options.SupportedUICultures = new[] { trCulture, enCulture };
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            };
        }
    }

    public static class Localization
    {
        public static IServiceCollection _ConfigureRequestLocalization(
            this IServiceCollection services,
            Action<RequestLocalizationOptions>? options = null)
        {
            services.Configure(options ?? RequestLocalizationOptionsDefaults.GetDefaults());
            return services;
        }

        public static IApplicationBuilder _UseRequestLocalization(this WebApplication app)
        {
            app.UseRequestLocalization();
            return app;
        }
    }
}
