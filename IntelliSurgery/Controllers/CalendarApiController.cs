using IntelliSurgery.DbOperations;
using IntelliSurgery.DTOs;
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
            List<ScheduledSurgery> surgeries = await surgeryRepository.GetAllSurgeries();
            ScheduledSurgeriesDTO scheduledSurgeriesDTO = new ScheduledSurgeriesDTO();
            scheduledSurgeriesDTO.ScheduledSurgeries = surgeries;
            return Json(new { success = true, data =  scheduledSurgeriesDTO});
        }
        
    }
}
