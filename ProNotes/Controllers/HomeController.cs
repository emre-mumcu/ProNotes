using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProNotes.AppData.EFCore.Context;
using ProNotes.AppLib;
using ProNotes.AppLib.MVC.Attributes;
using ProNotes.AppLib.MVC.Extensions;
using ProNotes.AppLib.MVC.Tools;
using System.Security.Claims;

namespace ProNotes.Controllers
{
    // [Authorize(Policy = nameof(AuthorizationPolicyLibrary.userPolicy))]
    public class HomeController : Controller
    {
        [MenuItem]
        public IActionResult Index([FromServices] AppDbContext dbContext)
        {
            // HttpContext.Session.SetKey<string>(AppConstants.SessionKey_System_Message, "Hello ;)");

            // If no role selected, select a role (default is User role):
            // var claims = (HttpContext?.User?.Identity as ClaimsIdentity)?.Claims;
            
            string? selectedRole = HttpContext.Session.GetKey<string>(AppConstants.SessionKey_SelectedRole);

            if (selectedRole == null)
                return RedirectToAction("SelectRole", new { id = "User" });

            return View(model: dbContext.Database.GetConnectionString());
        }

        public IActionResult SelectRole([Bind(Prefix = "id")] string roleName)
        {
            // rolename is encrypted by session id
            //string decryptedRole =
            //    new System.Net.NetworkCredential(HttpContext.Session.Id, StringCipher.DecryptSimpleURL(roleName, HttpContext.Session.Id))
            //    .Password;

            if (User.Identity != null && HttpContext != null)
                if (((System.Security.Claims.ClaimsIdentity)User.Identity).HasRole(roleName))
                {
                    HttpContext.Session.SetKey<string>(AppConstants.SessionKey_SelectedRole, roleName);
                }

            return RedirectToAction("Index", "Home");
        }
    }
}
