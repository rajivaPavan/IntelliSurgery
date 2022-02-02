using IntelliSurgery.DbOperations;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.DTOs;
using IntelliSurgery.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminApiController : Controller
    {
        private readonly ISurgeonRepository surgeonRepository;
        private readonly ISurgeryTypeRepository surgeryTypeRepository;
        private readonly ITheatreRepository theatreRepository;
        private readonly ISpecialityRepository specialityRepository;

        public AdminApiController(ISurgeonRepository surgeonRepository, ISurgeryTypeRepository surgeryTypeRepository, 
            ITheatreRepository theatreRepository, ISpecialityRepository specialityRepository
            )
        {
            this.surgeonRepository = surgeonRepository;
            this.surgeryTypeRepository = surgeryTypeRepository;
            this.theatreRepository = theatreRepository;
            this.specialityRepository = specialityRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetHospitalData()
        {
            List<TheatreDTO> theatreDTOs = new List<TheatreDTO>();
            List<Theatre> theatres = await theatreRepository.GetAllTheatres();
            foreach (var theatre in theatres)
            {
                theatreDTOs.Add(theatre.getDTO());
            }

            List<SurgeonDTO> surgeonDTOs = new List<SurgeonDTO>();
            List<Surgeon>  surgeons = await surgeonRepository.GetAllSurgeons();
            foreach (var surgeon in surgeons)
            {
                surgeonDTOs.Add(surgeon.getDTO());
            }

            HospitalDataDTO hospitalData = new HospitalDataDTO()
            {
                Specialities = await specialityRepository.GetAllSpecialities(),
                SurgeryTypes = await surgeryTypeRepository.GetAllSurgeryTypes(),
                TheatreTypes = await theatreRepository.GetAllTheatreTypes(),
                Theatres = theatreDTOs,
                Surgeons = surgeonDTOs
            };
            return Json(new { success = true, data = hospitalData });
        }

        [HttpPost]
        public async Task<IActionResult> SaveHospitalDate(HospitalDataDTO hospitalData)
        {
            List<Speciality> specialities = await AddSpecialities(hospitalData.Specialities);
            List<Surgeon> surgeons = await AddSurgeons(hospitalData.Surgeons);
            List<SurgeryType> surgeryTypes = await AddSurgeryTypes(hospitalData.SurgeryTypes);
            List<TheatreType> theatreTypes = await AddTheatreTypes(hospitalData.TheatreTypes);
            List<Theatre> theatres = await AddTheatres(hospitalData.Theatres);

            List<TheatreDTO> theatreDTOs = new List<TheatreDTO>();
            foreach (var theatre in theatres)
            {
                theatreDTOs.Add(theatre.getDTO());
            }

            List<SurgeonDTO> surgeonDTOs = new List<SurgeonDTO>();
            foreach (var surgeon in surgeons)
            {
                surgeonDTOs.Add(surgeon.getDTO());
            }

            hospitalData = new HospitalDataDTO()
            {
                Specialities = await specialityRepository.GetAllSpecialities(),
                SurgeryTypes = await surgeryTypeRepository.GetAllSurgeryTypes(),
                TheatreTypes = await theatreRepository.GetAllTheatreTypes(),
                Theatres = theatreDTOs,
                Surgeons = surgeonDTOs
            };

            return Json(new { success = true, data= hospitalData });
        }


        [NonAction]
        private async Task<List<SurgeryType>> AddSurgeryTypeTheatres(List<SurgeryTypeTheatresDTO> surgeryTypeTheatres)
        {
            List<SurgeryType> surgeryTypes = new List<SurgeryType>();
            foreach (var s in surgeryTypeTheatres)
            {
                SurgeryType surgeryType = await surgeryTypeRepository.GetSurgeryTypeById(s.SurgeryTypeId);
                List<TheatreType> theatreTypes = new List<TheatreType>();
                foreach (var tId in s.TheatreIds)
                {
                    theatreTypes.Add(await theatreRepository.GetTheatreType(t => t.Id == tId));
                }
                surgeryType.SuitableTheatreTypes = theatreTypes;
                surgeryTypes.Add(surgeryType);
            }

            surgeryTypes = await surgeryTypeRepository.UpdateSurgeryTypes(surgeryTypes);
            return surgeryTypes;
        }

        [NonAction]
        private async Task<List<Surgeon>> AddSurgeons(List<SurgeonDTO> surgeonDTOs)
        {
            List<Surgeon> surgeons = new List<Surgeon>();
            foreach(var s in surgeonDTOs)
            {
                Surgeon surgeon = new Surgeon()
                {
                    Name = s.Name,
                    Speciality = await specialityRepository.GetSpecialityById(s.SpecialityId),
                };
                surgeons.Add(surgeon);
            }
            
            return await surgeonRepository.AddSurgeons(surgeons);
        }
        
        [NonAction]
        private async Task<List<SurgeryType>> AddSurgeryTypes(List<SurgeryType> surgeryTypes)
        {
            return await surgeryTypeRepository.AddSurgeryTypes(surgeryTypes);
        }

        [NonAction]
        public async Task<List<Speciality>> AddSpecialities(List<Speciality> specialities)
        {
            return await specialityRepository.AddSpecialities(specialities);
        }

        [NonAction]
        public async Task<List<TheatreType>> AddTheatreTypes(List<TheatreType> theatreTypes)
        {
            return await theatreRepository.AddTheatreTypes(theatreTypes);
        }

        [NonAction]
        private async Task<List<Theatre>> AddTheatres(List<TheatreDTO> theatreDTOs)
        {
            List<Theatre> theatres = new List<Theatre>();
            foreach (var t in theatreDTOs)
            {
                Theatre theatre = new Theatre()
                {
                    Name = t.Name,
                    TheatreType = await theatreRepository.GetTheatreType( t1 => t1.Id == t.TheatreTypeId )
                };
                theatres.Add(theatre);
            }

            return await theatreRepository.AddTheatres(theatres);
        }

    }
}
