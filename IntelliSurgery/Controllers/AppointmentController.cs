using Microsoft.AspNetCore.Mvc;

namespace IntelliSurgery.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
