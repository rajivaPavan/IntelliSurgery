using Microsoft.AspNetCore.Mvc;

namespace IntelliSurgery.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
