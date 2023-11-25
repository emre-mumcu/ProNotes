using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ProNotes.AppLib.MVC.Defaults
{
    public class AuthenticationOptionsDefaults
    {
        public Action<AuthenticationOptions> GetDefaults()
        {
            Action<AuthenticationOptions> defaults = options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            };

            return defaults;
        }
    }
}