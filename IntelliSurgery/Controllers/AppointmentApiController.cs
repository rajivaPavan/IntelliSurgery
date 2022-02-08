using IntelliSurgery.DbOperations;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;
using IntelliSurgery.Global;
using IntelliSurgery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
        private readonly ITheatreRepository theatreRepository;
        private readonly ISpecialityRepository specialityRepository;
        private readonly IConfiguration configuration;

        public AppointmentApiController(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository,
            ISurgeonRepository surgeonRepository, ISurgeryTypeRepository surgeryRepository, ISurgeryTypeRepository surgeryTypeRepository,
            ITheatreRepository theatreRepository, ISpecialityRepository specialityRepository ,IConfiguration configuration)
        {

            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.surgeonRepository = surgeonRepository;
            this.surgeryRepository = surgeryRepository;
            this.surgeryTypeRepository = surgeryTypeRepository;
            this.theatreRepository = theatreRepository;
            this.specialityRepository = specialityRepository;
            this.configuration = configuration;
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

                List<int> diseaseValues = new List<int>();
                patient.Diseases.ForEach(d => diseaseValues.Add((int)d.DiseaseEnum));

                patientDTO = new PatientDTO()
                {
                    DateOfBirth = patient.DateOfBirth,
                    Gender = (int)patient.Gender,
                    Height = patient.Height,
                    Weight = patient.Weight,
                    Name = patient.Name,
                    AsaStatus = (int)patient.AsaStatus,
                    DiseasesValues = diseaseValues
                };
            }
            
            return Json(new {success =  true, data = isPatientExist , patient = patientDTO});
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient(PatientDTO patientDTO)
        {
            List<Disease> diseases = new List<Disease>();
            foreach(int d in patientDTO.DiseasesValues)
            {
                Disease disease = await patientRepository.GetDiseaseByEnumValue((DiseaseEnum)d);
                diseases.Add(disease);  
            }

            Patient newPatient = new Patient()
            {
                DateOfBirth = patientDTO.DateOfBirth,
                Gender = (Gender)patientDTO.Gender,
                Weight = patientDTO.Weight,
                Name = patientDTO.Name,
                Height = patientDTO.Height,
                BMI = (float)(patientDTO.Weight / Math.Pow(patientDTO.Height, 2)),
                Diseases = diseases,
                AsaStatus = (ASA_Status)patientDTO.AsaStatus
            };
            newPatient = await patientRepository.CreatePatient(newPatient);
            return Json(new {success= true, data = newPatient.Id });
        }

        public async Task<IActionResult> UpdatePatient(PatientDTO patientDTO)
        {
            Patient updatePatient = await patientRepository.GetPatientById(patientDTO.PatientId);
            updatePatient.Weight = patientDTO.Weight;
            updatePatient.Height = patientDTO.Height;
            updatePatient.BMI = (float)(patientDTO.Weight / Math.Pow(patientDTO.Height, 2));
            updatePatient.AsaStatus = (ASA_Status)patientDTO.AsaStatus;
            List<Disease> diseases = new List<Disease>();
            foreach (int d in patientDTO.DiseasesValues)
            {
                Disease disease = await patientRepository.GetDiseaseByEnumValue((DiseaseEnum)d);
                diseases.Add(disease);
            }
            updatePatient.Diseases = diseases;
            updatePatient = await patientRepository.UpdatePatient(updatePatient);
            return Json(new { success = true, data = updatePatient.Id });
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment(AppointmentDTO appointmentDTO) {

            PythonScript pythonScript = new PythonScript(configuration);

            Patient patient = await patientRepository.GetPatientById(appointmentDTO.PatientId);
            SurgeryType surgerytype = await surgeryRepository.GetSurgeryTypeById(appointmentDTO.SurgeryType);
            Surgeon surgeon = await surgeonRepository.GetSurgeonById(appointmentDTO.SurgeonId);
            TheatreType theatreType = await theatreRepository.GetTheatreType(TheatreTypeQueryLogic.ById(appointmentDTO.TheatreType));

            //create appointment object
            Appointment appointment = new Appointment() {
                Patient = patient,
                PatientId = patient.Id,
                Surgeon = surgeon,
                SurgeonId = surgeon.Id,
                SurgeryType = surgerytype,
                SurgeryTypeId = surgerytype.Id,
                TheatreType = theatreType,
                TheatreTypeId = theatreType.Id,
                ComplicationPossibility = appointmentDTO.Complication,
                AnesthesiaType = (AnesthesiaType)appointmentDTO.AnesthesiaType,
                PriorityLevel = (PriorityLevel)appointmentDTO.PriorityLevel,
                Status = Status.Pending,
                DateAdded = DateTime.Now,
            };

            PatientMedicalData timePredictionDTO = new PatientMedicalData(patient, appointment);
            TimeSpan predictedTime = pythonScript.PredictTime(timePredictionDTO);

            appointment.SystemPredictedDuration = predictedTime;

            //save appointment in database
            appointment = await appointmentRepository.AddAppointment(appointment);

            //return predicted Time
            return Json(new { success = true, data = appointment.SystemPredictedDuration, appointmentId = appointment.Id }) ;
        }

        //called on page load
        [HttpGet]
        public async Task<IActionResult> GetFormDropDownLists()
        {
            //surgery types and surgeon list
            var surgeons = await surgeonRepository.GetAllSurgeons();
            var surgeryTypes = await surgeryTypeRepository.GetAllSurgeryTypes();
            var theatreTypes = await theatreRepository.GetAllTheatreTypes();
            var specialities = await specialityRepository.GetAllSpecialities();
            List<AnesthesiaDTO> anesthesias = new List<AnesthesiaDTO>();
            foreach(AnesthesiaType item in Enum.GetValues(typeof(AnesthesiaType))){
                anesthesias.Add(new AnesthesiaDTO(item));
            }

            List<SurgeonDTO> surgeonDTOs = new List<SurgeonDTO>();
            foreach (var s in surgeons) {
                surgeonDTOs.Add(s.getDTO());
            }

            DropDownListsDTO dropDownLists = new DropDownListsDTO() { 
                SurgeryTypes = surgeryTypes, 
                Surgeons = surgeonDTOs,
                TheatreTypes = theatreTypes,
                Anesthesias = anesthesias,
                Specialities = specialities
            };

            return Json(new { success = true, data = dropDownLists});
        }

        [HttpPost]
        public async Task<IActionResult> OverrideTimeDuration(int appointmentId, string timeDuration)
        {
            Appointment appointment = await appointmentRepository.GetAppointment(a => a.Id == appointmentId);
            appointment.SurgeonsPredictedDuration = TimeSpan.Parse(timeDuration);
            await appointmentRepository.UpdateAppointment(appointment);
            return Json(new { success = true });
        }
    }
}
