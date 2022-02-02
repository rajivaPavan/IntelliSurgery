using IntelliSurgery.DbOperations;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.Global;
using IntelliSurgery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SurgeriesApiController : Controller
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly ISurgeryScheduler surgeryScheduler;
        private readonly ISurgeonRepository surgeonRepository;

        public SurgeriesApiController(IAppointmentRepository appointmentRepository,
            ISurgeryScheduler surgeryScheduler, ISurgeonRepository surgeonRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.surgeryScheduler = surgeryScheduler;
            this.surgeonRepository = surgeonRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTableData()
        {
            List<Appointment> appointments = await appointmentRepository.GetAllAppointments();
            return Json(new { success = true, data = appointments });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(int surgeonId)
        {
            Surgeon surgeon = await surgeonRepository.GetSurgeonById(surgeonId);
            await surgeryScheduler.CreateSchedule(surgeon);
            return Json(new { success = true });
        }

    }
}
