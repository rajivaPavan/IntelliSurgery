using IntelliSurgery.DbOperations;
using Microsoft.AspNetCore.Mvc;

namespace IntelliSurgery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SurgeriesApiController : Controller
    {
        private readonly ISurgeryTypeRepository surgeryRepository;

        public SurgeriesApiController(ISurgeryTypeRepository surgeryRepository)
        {
            this.surgeryRepository = surgeryRepository;
        }
    }
}
