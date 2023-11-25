using Microsoft.AspNetCore.Mvc;

namespace ProNotes.Controllers
{
    public class ImageGalleryController : Controller
    {
        public IActionResult Index() => View();
    }
}
