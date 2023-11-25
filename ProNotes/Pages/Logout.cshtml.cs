using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProNotes.AppLib;
using ProNotes.AppLib.MVC.Extensions;
using System.Security.Claims;

namespace ProNotes.Pages
{
    [AllowAnonymous]
    public partial class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            return await LogoutUser();
        }
    }

    public partial class LogoutModel
    {
        [NonAction]
        private async Task<RedirectResult> LogoutUser()
        {
            HttpContext.Session.RemoveKey(AppConstants.SessionKey_Login);
            HttpContext.Session.RemoveKey(AppConstants.SessionKey_LoginUser);

            HttpContext.User = new ClaimsPrincipal();

            HttpContext.Session.Clear();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var session_cookie = HttpContext.Request.Cookies[AppConstants.Session_Cookie_Name];
            if (session_cookie != null)
            {
                var options = new CookieOptions { Expires = DateTime.Now.AddDays(-1) };
                HttpContext.Response.Cookies.Append(AppConstants.Session_Cookie_Name, session_cookie, options);
            }

            var auth_cookie = HttpContext.Request.Cookies[AppConstants.Auth_Cookie_Name];
            if (auth_cookie != null)
            {
                var options = new CookieOptions { Expires = DateTime.Now.AddDays(-1) };
                HttpContext.Response.Cookies.Append(AppConstants.Session_Cookie_Name, auth_cookie, options);
            }

            return new RedirectResult("/Login");
        }
    }
}
