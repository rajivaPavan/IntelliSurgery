using Microsoft.AspNetCore.Mvc;

namespace IntelliSurgery.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
