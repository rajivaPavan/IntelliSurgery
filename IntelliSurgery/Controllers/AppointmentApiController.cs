using IntelliSurgery.DbOperations;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;
using IntelliSurgery.Global;
using IntelliSurgery.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static IntelliSurgery.Enums.OperationTheatreEnums;

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
        private readonly ITheatreRepository theatreRepository;

        public AppointmentApiController(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository,
            ISurgeonRepository surgeonRepository, ISurgeryTypeRepository surgeryRepository, ISurgeryTypeRepository surgeryTypeRepository,
            ITheatreRepository theatreRepository)
        {

            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.surgeonRepository = surgeonRepository;
            this.surgeryRepository = surgeryRepository;
            this.surgeryTypeRepository = surgeryTypeRepository;
            this.theatreRepository = theatreRepository;
        }

        [HttpPost] 
        public async Task<IActionResult> ValidatePatient(int patientId)
        {
            Patient patient = await patientRepository.GetPatientById(patientId);
            bool isPatientExist = false;
            PatientDTO patientDTO = null;

            if (patient != null)
            {
                isPatientExist = true;
                patientDTO = new PatientDTO()
                {
                    DateOfBirth = patient.DateOfBirth,
                    Gender = (int)patient.Gender,
                    Height = patient.Height,
                    Weight = patient.Weight,
                    Name = patient.Name
                };
            }
            
            return Json(new {success =  true, data = isPatientExist , patient = patientDTO});
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient(PatientDTO patientDTO)
        {
            Patient newPatient = new Patient()
            {
                DateOfBirth = patientDTO.DateOfBirth,
                Gender = (Gender)patientDTO.Gender,
                Weight = patientDTO.Weight,
                Name = patientDTO.Name,
                Height = patientDTO.Height
            };
            newPatient = await patientRepository.CreatePatient(newPatient);
            return Json(new {success= true, data = newPatient.Id });
        }

        public async Task<IActionResult> UpdatePatient(PatientDTO patientDTO)
        {
            Patient updatePatient = new Patient()
            {
                Weight = patientDTO.Weight,
                Height = patientDTO.Height
            };
            updatePatient = await patientRepository.UpdatePatient(updatePatient);
            return Json(new { success = true, data = updatePatient.Id });
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment(AppointmentDTO appointmentDTO) {

            PythonScript pythonScript = new PythonScript();

            Patient patient = await patientRepository.GetPatientById(appointmentDTO.PatientId);
            SurgeryType surgerytype = await surgeryRepository.GetSurgeryTypeById(appointmentDTO.SurgeryType);
            Surgeon surgeon = await surgeonRepository.GetSurgeonById(appointmentDTO.SurgeonId);
            TimeSpan predictedTime = pythonScript.PredictTime();
            TheatreType theatreType = await theatreRepository.GetTheatreType(TheatreTypeQueryLogic.ById(appointmentDTO.TheatreType));

            //create appointment object
            Appointment appointment = new Appointment() {
                Patient = patient,
                Surgeon = surgeon,
                SurgeryType = surgerytype,
                TheatreType = theatreType,
                AnesthesiaType = (AnesthesiaType)appointmentDTO.AnesthesiaType,
                PriorityLevel = (PriorityLevel)appointmentDTO.PriorityLevel,
                PredictedTimeDuration = predictedTime,
                Status = Status.Pending,
                DateAdded = DateTime.Now,
                Priority = null
            };
            
            //save appointment in database
            await appointmentRepository.CreateAppointment(appointment);

            //return predicted Time
            return Json(new { success = true, data = appointment.PredictedTimeDuration }) ;
        }

        //called on page load
        [HttpGet]
        public async Task<IActionResult> GetFormDropDownLists()
        {
            //surgery types and surgeon list
            var surgeons = await surgeonRepository.GetAllSurgeons();
            var surgeryTypes = await surgeryTypeRepository.GetAllSurgeryTypes();
            var theatreTypes = await theatreRepository.GetAllTheatreTypes();
            List<AnesthesiaDTO> anesthesias = new List<AnesthesiaDTO>();
            foreach(AnesthesiaType item in Enum.GetValues(typeof(AnesthesiaType))){
                anesthesias.Add(new AnesthesiaDTO(item));
            }

            DropDownListsDTO dropDownLists = new DropDownListsDTO() { 
                SurgeryTypes = surgeryTypes, 
                Surgeons = surgeons,
                TheatreTypes = theatreTypes,
                Anesthesias = anesthesias
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
