using IntelliSurgery.DbOperations;
using IntelliSurgery.DTOs;
using IntelliSurgery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IntelliSurgery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminApiController : Controller
    {
        private readonly ISurgeonRepository surgeonRepository;
        private readonly ISurgeryTypeRepository surgeryTypeRepository;

        public AdminApiController(ISurgeonRepository surgeonRepository, ISurgeryTypeRepository surgeryTypeRepository)
        {
            this.surgeonRepository = surgeonRepository;
            this.surgeryTypeRepository = surgeryTypeRepository;
        }

        public async Task<IActionResult> AddSurgeon(SurgeonDTO surgeonDTO)
        {
            Surgeon surgeon = new Surgeon() 
            {
                Name = surgeonDTO.SurgeonName
            };
            await surgeonRepository.AddSurgeon(surgeon);
            return Json(new { success = true });
        }

        public async Task<IActionResult> AddSurgeryType(SurgeryTypeDTO surgeryTypeDTO)
        {
            SurgeryType surgeryType = new SurgeryType() { Name = surgeryTypeDTO.SurgeryTypeName };
            await surgeryTypeRepository.AddSurgeryType(surgeryType);
            return Json(new { success = true });
        }
    }
}
