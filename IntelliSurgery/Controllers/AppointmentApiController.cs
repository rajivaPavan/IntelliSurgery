using IntelliSurgery.DbOperations;
using IntelliSurgery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IntelliSurgery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentApiController(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        public async Task<IActionResult> SubmitForm()
        {
            Appointment appointment = new Appointment();
            await appointmentRepository.CreateAppointment(appointment);
            return Json(new { success = true }) ;
        }
    }
}
