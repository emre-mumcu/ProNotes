using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using ProNotes.AppLib.MVC.Attributes;

namespace ProNotes.AppLib.MVC.Defaults
{
    public class MvcOptionsDefaults
    {
        public Action<MvcOptions> GetDefaults()
        {
            Action<MvcOptions> defaults = options =>
            {
                options.Filters.Add(new CustomHeadersAttribute());
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.Filters.Add(new AuthorizeFilter());
            };

            return defaults;
        }
    }
}
