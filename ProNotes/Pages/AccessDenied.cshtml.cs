using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProNotes.Pages
{
    [AllowAnonymous]
    public class AccessDeniedModel : PageModel
    {
        public void OnGet([FromServices] IHttpContextAccessor httpContextAccessor)
        {
            //if(httpContextAccessor.HttpContext != null && !httpContextAccessor.HttpContext.Session.ValidateUserSession())
            //{
            //    return Redirect("~/Login");
            //}

            //return Page();
        }
    }
}
