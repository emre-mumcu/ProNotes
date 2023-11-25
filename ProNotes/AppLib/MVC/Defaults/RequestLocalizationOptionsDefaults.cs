using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace ProNotes.AppLib.MVC.Defaults
{
    public class RequestLocalizationOptionsDefaults
    {
        public Action<RequestLocalizationOptions> GetDefaults()
        {
            Action<RequestLocalizationOptions> defaults = options =>
            {
                options.DefaultRequestCulture = new RequestCulture(new CultureInfo("tr-TR"));
                options.SupportedCultures = new[] { new CultureInfo("tr-TR") };
                options.SupportedUICultures = new[] { new CultureInfo("tr-TR") };
                options.RequestCultureProviders = new List<IRequestCultureProvider> {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                };
            };

            return defaults;
        }
    }
}
