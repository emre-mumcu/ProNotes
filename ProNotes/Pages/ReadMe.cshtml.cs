using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProNotes.Pages
{
    [AllowAnonymous]
    public class ReadMeModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
