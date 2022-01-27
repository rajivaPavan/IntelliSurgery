using IntelliSurgery.DbOperations;
using IntelliSurgery.Global;
using IntelliSurgery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static IntelliSurgery.Enums.OperationTheatreEnums;

namespace IntelliSurgery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SurgeriesApiController : Controller
    {
        private readonly ISurgeryRepository surgeryRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly ISurgeryScheduler surgeryScheduler;

        public SurgeriesApiController(ISurgeryRepository surgeryRepository, IAppointmentRepository appointmentRepository,
            ISurgeryScheduler surgeryScheduler)
        {
            this.surgeryRepository = surgeryRepository;
            this.appointmentRepository = appointmentRepository;
            this.surgeryScheduler = surgeryScheduler;
        }

        [HttpGet]
        public async Task<IActionResult> GetTableData()
        {
            List<Appointment> appointments = await appointmentRepository.GetAllAppointments();
            return Json(new { success = true, data = appointments });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(int theaterType)
        {           
            await surgeryScheduler.CreateSchedule((TheatreType)theaterType);
            return Json(new { success = true });
        }

    }
}
