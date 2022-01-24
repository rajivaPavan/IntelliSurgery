using Microsoft.AspNetCore.Mvc;

namespace IntelliSurgery.Controllers
{
    public class SurgeriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
