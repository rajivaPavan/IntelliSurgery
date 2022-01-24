using IntelliSurgery.DbOperations;
using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IntelliSurgery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPatientRepository patientRepository;
        private readonly ISurgeonRepository surgeonRepository;
        private readonly ISurgeryRepository surgeryRepository;

        public AppointmentApiController(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository,
            ISurgeonRepository surgeonRepository, ISurgeryRepository surgeryRepository)
        {

            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.surgeonRepository = surgeonRepository;
            this.surgeryRepository = surgeryRepository;
        }

        public async Task<IActionResult> SubmitForm(AppointmentDTO appointmentDTO) {

            Patient patient = await patientRepository.GetPatientById(appointmentDTO.PatientId);
            //get surgerytype
            //get surgeon

            //create appointment object
            Appointment appointment = new Appointment() {
                Patient = patient,
                //Surgeon = 
                //SurgeryType =
                AnesthesiaType = (AnesthesiaType)Enum.Parse(typeof(AnesthesiaType), appointmentDTO.AnesthesiaType, true),
                PriorityLevel = (PriorityLevel)Enum.Parse(typeof(PriorityLevel), appointmentDTO.PriorityLevel,true)
            };
            
            //save appointment in database
            await appointmentRepository.CreateAppointment(appointment);

            return Json(new { success = true }) ;
        }
    }
}
