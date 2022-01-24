using IntelliSurgery.DbOperations;
using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;
using IntelliSurgery.Global;
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

        [HttpPost]
        public async Task<IActionResult> AddAppointment(AppointmentDTO appointmentDTO) {

            PythonScript pythonScript = new PythonScript();

            Patient patient = await patientRepository.GetPatientById(appointmentDTO.PatientId);
            SurgeryType surgerytype = await surgeryRepository.GetSurgeryTypeById(appointmentDTO.SurgeryType);
            Surgeon surgeon = await surgeonRepository.GetSurgeonById(appointmentDTO.DoctorId);
            TimeSpan predictedTime = pythonScript.PredictTime();

            //create appointment object
            Appointment appointment = new Appointment() {
                Patient = patient,
                Surgeon = surgeon,
                SurgeryType = surgerytype,
                AnesthesiaType = (AnesthesiaType)Enum.Parse(typeof(AnesthesiaType), appointmentDTO.AnesthesiaType, true),
                PriorityLevel = (PriorityLevel)Enum.Parse(typeof(PriorityLevel), appointmentDTO.PriorityLevel,true),
                PredictedTimeDuration = predictedTime
            };
            
            //save appointment in database
            await appointmentRepository.CreateAppointment(appointment);

            //return predicted Time
            return Json(new { success = true }) ;
        }

        ////called on page load
        //[HttpGet]
        //public async Task<IActionResult> GetFormDropDownLists()
        //{
        //    //surgery types and surgeon list
        //    //write a new dto class that with fields of type two list of above types of data

        //    return Json(new { success = true /* ,data = */});
        //}

        ////on patientload
        ////allergies, diseases
        //public async Task<IActionResult> GetPatientHistory(PatientDTO patientDto)
        //{
        //      find patient and read patient allergies and dieseases
        //    //write a class for patient history
        //    //create a patient history object and send that as data
            
        //}
    }
}
