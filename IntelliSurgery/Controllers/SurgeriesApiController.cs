using IntelliSurgery.DbOperations;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.DTOs;
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

        [HttpPost]
        public async Task<IActionResult> GetTableData(int surgeonId)
        {
            List<Appointment> appointments = await appointmentRepository.GetAppointments(a => a.SurgeonId == surgeonId);
            List< AppointmentExtendedProp> records = new List<AppointmentExtendedProp>();
            foreach (Appointment appointment in appointments)
            {
                records.Add(new AppointmentExtendedProp(appointment));
            }
            return Json(new { success = true, data = records });
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
