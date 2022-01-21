using IntelliSurgery.DbOperations;
using Microsoft.AspNetCore.Mvc;

namespace IntelliSurgery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CalendarApiController : Controller
    {
        private readonly ICalendarRepository calendarRepository;

        public CalendarApiController(ICalendarRepository calendarRepository)
        {
            this.calendarRepository = calendarRepository;
        }
        
    }
}
