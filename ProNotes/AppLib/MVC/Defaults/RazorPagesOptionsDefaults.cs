using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProNotes.AppLib.MVC.Attributes;

namespace ProNotes.AppLib.MVC.Defaults
{
    public class RazorPagesOptionsDefaults
    {
        public Action<RazorPagesOptions> GetDefaults()
        {
            Action<RazorPagesOptions> defaults = options =>
            {
                options.Conventions.ConfigureFilter(new CustomHeadersAttribute());
                options.Conventions.ConfigureFilter(new AutoValidateAntiforgeryTokenAttribute());
                options.Conventions.ConfigureFilter(new AuthorizeFilter());

            };

            return defaults;
        }
    }
}
