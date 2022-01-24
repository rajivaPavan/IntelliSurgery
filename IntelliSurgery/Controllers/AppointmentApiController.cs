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
        private readonly ISurgeryTypeRepository surgeryRepository;

        public AppointmentApiController(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository,
            ISurgeonRepository surgeonRepository, ISurgeryTypeRepository surgeryRepository)
        {

            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.surgeonRepository = surgeonRepository;
            this.surgeryRepository = surgeryRepository;
        }

        public async Task<IActionResult> AddAppointment(AppointmentDTO appointmentDTO) {

            Patient patient = await patientRepository.GetPatientById(appointmentDTO.PatientId);
            SurgeryType surgerytype = await surgeryRepository.GetSurgeryTypeById(appointmentDTO.SurgeryType);
            Surgeon surgeon = await surgeonRepository.GetSurgeonById(appointmentDTO.DoctorId);

            //create appointment object
            Appointment appointment = new Appointment() {
                Patient = patient,
                Surgeon = surgeon,
                SurgeryType = surgerytype,
                AnesthesiaType = (AnesthesiaType)Enum.Parse(typeof(AnesthesiaType), appointmentDTO.AnesthesiaType, true),
                PriorityLevel = (PriorityLevel)Enum.Parse(typeof(PriorityLevel), appointmentDTO.PriorityLevel,true)
            };
            
            //save appointment in database
            await appointmentRepository.CreateAppointment(appointment);

            return Json(new { success = true }) ;
        }
    }
}
