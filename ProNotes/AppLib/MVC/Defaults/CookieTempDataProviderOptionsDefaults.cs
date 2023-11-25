using Microsoft.AspNetCore.Mvc;

namespace ProNotes.AppLib.MVC.Defaults
{
    public class CookieTempDataProviderOptionsDefaults
    {
        public Action<CookieTempDataProviderOptions> GetDefaults()
        {
            Action<CookieTempDataProviderOptions> defaults = options =>
            {
                options.Cookie.IsEssential = true;
            };

            return defaults;
        }
    }
}