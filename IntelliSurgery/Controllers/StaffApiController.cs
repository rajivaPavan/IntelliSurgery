using IntelliSurgery.DbOperations;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.DbOperations.WorkingBlocks;
using IntelliSurgery.DTOs;
using IntelliSurgery.Models;
using Itenso.TimePeriod;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StaffApiController : Controller
    {
        private readonly IWorkingBlockRepository workingBlockRepository;
        private readonly ISurgeonRepository surgeonRepository;
        private readonly ITheatreRepository theatreRepository;

        public StaffApiController(IWorkingBlockRepository workingBlockRepository, ISurgeonRepository surgeonRepository,
            ITheatreRepository theatreRepository)
        {
            this.workingBlockRepository = workingBlockRepository;
            this.surgeonRepository = surgeonRepository;
            this.theatreRepository = theatreRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SaveWorkingBlock(WorkBlockDTO workBlockDTO)
        {
            Surgeon surgeon = await surgeonRepository.GetSurgeonById(workBlockDTO.SurgeonId);
            TimeRange timeRange = new TimeRange() { Start = workBlockDTO.Start, End = workBlockDTO.End };
            //VALIDATE whether timerange overlaps with any other block
            //if overlap occurs return json success = false


            //else proceed to create and save the block
            Theatre theatre = await theatreRepository.GetTheatre(TheatreQueryLogic.ById(workBlockDTO.TheatreId));

            WorkingBlock workingBlock = new WorkingBlock() { Surgeon = surgeon, Theatre = theatre};
            workingBlock.SetTimeRange(timeRange);
            workingBlock.RemainingTime = timeRange.Duration;
            
            workingBlock = await workingBlockRepository.AddWorkingBlock(workingBlock);

            SurgeonCalendarEvent surgeonCalendarEvent = new SurgeonCalendarEvent(workingBlock);

            return Json(new { success = true, data= surgeonCalendarEvent });
        }

        [HttpPost]
        public async Task<IActionResult> GetWorkingBlocks(int surgeonId)
        {
            Surgeon surgeon = await surgeonRepository.GetSurgeonById(surgeonId);
            List<WorkingBlock> workingBlocks = await workingBlockRepository.GetWorkBlocks(surgeon);
            List<SurgeonCalendarEvent> events = new List<SurgeonCalendarEvent>();
            foreach (WorkingBlock workingBlock in workingBlocks)
            {
                events.Add(new SurgeonCalendarEvent(workingBlock));
            }

            SurgeonCalendarDTO surgeonCalendarDTO = new SurgeonCalendarDTO() { 
                Surgeon = surgeon,
                Events = events
            };
            
            return Json(new { success = true, data = surgeonCalendarDTO});
        }
    }
}
