using IntelliSurgery.DbOperations;
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
        private readonly ISurgeryRepository surgeryRepository;
        private readonly IAppointmentRepository appointmentRepository;

        public SurgeriesApiController(ISurgeryRepository surgeryRepository, IAppointmentRepository appointmentRepository)
        {
            this.surgeryRepository = surgeryRepository;
            this.appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTableData()
        {
            List<Appointment> appointments = await appointmentRepository.GetAppointments();
            return Json(new { success = true, data = appointments });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule()
        {
            List<Appointment> appointments = await appointmentRepository.GetAppointments();
            
            SurgeryScheduler scheduler = new SurgeryScheduler(surgeryRepository);
            await scheduler.CreateSchedule(appointments);
            return Json(new { success = true });
        }

    }
}
