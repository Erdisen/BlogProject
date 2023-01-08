using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
