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
        private readonly ISurgeryRepository surgeryRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly ISurgeryScheduler surgeryScheduler;
        private ITheatreRepository theatreRepository;

        public SurgeriesApiController(ISurgeryRepository surgeryRepository, IAppointmentRepository appointmentRepository,
            ISurgeryScheduler surgeryScheduler, ITheatreRepository theatreRepository)
        {
            this.surgeryRepository = surgeryRepository;
            this.appointmentRepository = appointmentRepository;
            this.surgeryScheduler = surgeryScheduler;
            this.theatreRepository = theatreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTableData()
        {
            List<Appointment> appointments = await appointmentRepository.GetAllAppointments();
            return Json(new { success = true, data = appointments });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(int theaterTypeId)
        {
            TheatreType theatreType = await theatreRepository.GetTheatreType(TheatreTypeQueryLogic.ById(theaterTypeId));
            await surgeryScheduler.CreateSchedule(theatreType);
            return Json(new { success = true });
        }

    }
}
