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
        private readonly ISurgeryTypeRepository surgeryTypeRepository;

        public AppointmentApiController(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository,
            ISurgeonRepository surgeonRepository, ISurgeryTypeRepository surgeryRepository, ISurgeryTypeRepository surgeryTypeRepository)
        {

            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.surgeonRepository = surgeonRepository;
            this.surgeryRepository = surgeryRepository;
            this.surgeryTypeRepository = surgeryTypeRepository;
        }

        [HttpPost] 
        public async Task<IActionResult> ValidatePatient(PatientDTO patientDTO)
        {
            Patient patient = await patientRepository.GetPatientById(patientDTO.PatientId);
            bool isPatientExist = true;
            if (patient == null)
            {
                isPatientExist = false;
            }
            return Json(new {success =  true, data = isPatientExist});
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient(PatientDTO patientDTO)
        {
            Patient newPatient = new Patient()
            {
                Age = patientDTO.Age,
                Gender = (Gender)patientDTO.Gender,
                Weight = patientDTO.Weight,
                Name = patientDTO.Name
            };
            newPatient = await patientRepository.CreatePatient(newPatient);
            return Json(new {success= true, data = newPatient.Id });
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

        //called on page load
        [HttpGet]
        public async Task<IActionResult> GetFormDropDownLists()
        {
            //surgery types and surgeon list
            var surgeons = await surgeonRepository.GetSurgeons();
            var surgeryTypes = await surgeryTypeRepository.GetSurgeryTypes();

            DropDownListsDTO dropDownLists = new DropDownListsDTO() { 
                SurgeryTypes = surgeryTypes, 
                Surgeons = surgeons 
            };

            return Json(new { success = true, data = dropDownLists});
        }

        //on patientload
        //allergies, diseases
        //public async Task<IActionResult> GetPatientHistory(PatientDTO patientDto)
        //{
        //    //find patient and read patient allergies and dieseases
        //    //write a class for patient history
        //    //create a patient history object and send that as data
        //}
    }
}
