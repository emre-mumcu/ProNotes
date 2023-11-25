using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProNotes.AppLib;
using ProNotes.AppLib.Abstract;
using ProNotes.AppLib.MVC.Extensions;
using ProNotes.AppLib.MVC.ViewModels;
using ProNotes.AppLib.Tools;
using System.Security.Claims;

namespace ProNotes.Pages
{
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public partial class LoginModel : PageModel
    {
        [BindProperty]
        public LoginViewModel loginViewModel { get; set; } = new LoginViewModel();

        private readonly IAuthenticate _authenticator;
        private readonly IAuthorize _authorizer;

        public LoginModel(IAuthenticate authenticator, IAuthorize authorizer)
        {
            _authenticator = authenticator;
            _authorizer = authorizer;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.ValidateUserSession()) return RedirectToAction("Index", "Home");
            return Page();
        }

        public async Task<IActionResult> OnPost([Bind(Prefix = "loginViewModel")] LoginViewModel model, string? returnurl)
        {
            void ClearCaptchaText()
            {
                model.Captcha.CaptchaCode = string.Empty;
                ModelState.Remove("Captcha.CaptchaCode");
            }

            if (!ModelState.IsValid)
            {
                ClearCaptchaText();
                ModelState.AddModelError("ERR", $"Modelstate is not valid");
                return Page();
            }
            else
            {
                try
                {
                    if (HttpContext.Session.GetKey<string>(AppConstants.SessionKey_Captcha) != model.Captcha.CaptchaCode)
                    {
                        ClearCaptchaText();
                        ModelState.AddModelError("Captcha", "Invalid CAPTCHA");
                        return Page();
                    }
                    else
                    {
                        var result = await LoginUser(model.Username, model.Password, model.RememberMe);

                        if (result.loginResult)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ClearCaptchaText();
                            ModelState.AddModelError("EX", result.loginMessage);
                            return Page();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ClearCaptchaText();
                    ModelState.AddModelError("EX", ex.Message);
                    return Page();
                }
            }
        }

#if DEBUG
#endif
        // TODO !!! WARNING AutoLogin is ENABLED
        public async Task OnPostDevelopment(string Username, string Password, bool Remember)
        {            
            var result = await LoginUser(Username, Password, Remember);            
        }


        // https://localhost:44396/Login?handler=CaptchaImage
        // https://localhost:44396/Login/CaptchaImage
        public IActionResult OnGetCaptchaImage()
        {
            var result = Captcha2.GenerateCaptchaImage();
            HttpContext.Session.SetKey<string>(AppConstants.SessionKey_Captcha, result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            //return File(s, "image/png");
            return new FileStreamResult(s, "image/png");
        }
    }

    public partial class LoginModel
    {
        [NonAction]
        private async Task<(bool loginResult, string loginMessage)> LoginUser(string username, string password, bool remember)
        {
            if (_authenticator.AuthenticateUser(username, password))
            {
                AuthenticationTicket? ticket = await _authorizer.AuthorizeUserAsync(username);

                if (ticket != null)
                {
                    HttpContext.Session.CreateUserSession(ticket.Principal.FindFirstValue(ClaimTypes.NameIdentifier));

                    await HttpContext.SignInAsync(
                        ticket.AuthenticationScheme,
                        ticket.Principal,
                        ticket.Properties
                    );

                    return (true, string.Empty);
                }
                else
                {
                    return (false, "User is not allowed");
                }
            }
            else
            {
                return (false, "Invalid user credentials");
            }
        }
    }
}