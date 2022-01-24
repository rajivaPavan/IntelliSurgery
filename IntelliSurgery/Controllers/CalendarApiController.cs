using IntelliSurgery.DbOperations;
using IntelliSurgery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CalendarApiController : Controller
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly ISurgeryRepository surgeryRepository;

        public CalendarApiController(ICalendarRepository calendarRepository,ISurgeryRepository surgeryRepository)
        {
            this.calendarRepository = calendarRepository;
            this.surgeryRepository = surgeryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetScheduledSurgeries()
        {
            //List<ScheduledSurgery> surgeries = await surgeryRepository.();

            return Json(new { success = true  /* , data = */ });
        }
        
    }
}
