using IntelliSurgery.DbOperations;
using Microsoft.AspNetCore.Mvc;

namespace IntelliSurgery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SurgeriesApiController : Controller
    {
        private readonly ISurgeryRepository surgeryRepository;

        public SurgeriesApiController(ISurgeryRepository surgeryRepository)
        {
            this.surgeryRepository = surgeryRepository;
        }
    }
}
