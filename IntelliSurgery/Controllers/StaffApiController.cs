using IntelliSurgery.DbOperations;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.DbOperations.WorkingBlocks;
using IntelliSurgery.DTOs;
using IntelliSurgery.Logic;
using IntelliSurgery.Models;
using Itenso.TimePeriod;
using Microsoft.AspNetCore.Mvc;
using System;
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
            TimeRange timeRange = new TimeRange() { 
                Start = DateTime.Parse(workBlockDTO.Start), 
                End = DateTime.Parse(workBlockDTO.End)
            };

            //VALIDATE whether timerange overlaps with any other block
            //if overlap occurs return json success = false
            bool isOverLapping = await CheckIfBlockOverlapsAsync(timeRange, workBlockDTO.TheatreId, surgeon.Id);
            if (isOverLapping)
            {
                return Json(new { success = false });
            }

            //else proceed to create and save the block
            Theatre theatre = await theatreRepository.GetTheatre(TheatreQueryLogic.ById(workBlockDTO.TheatreId));
            
            WorkingBlock workingBlock = new WorkingBlock(surgeon, theatre, timeRange);
            workingBlock = await workingBlockRepository.AddWorkingBlock(workingBlock);

            SurgeonCalendarEvent surgeonCalendarEvent = new SurgeonCalendarEvent(workingBlock);

            return Json(new { success = true, data= surgeonCalendarEvent });
        }

        [HttpPost]
        public async Task<IActionResult> GetWorkingBlocks(int surgeonId)
        {
            Surgeon surgeon = await surgeonRepository.GetSurgeonById(surgeonId);
            List<WorkingBlock> workingBlocks = await workingBlockRepository.GetWorkBlocks(w => w.Surgeon.Id == surgeon.Id);
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

        [HttpPost]
        public async Task<IActionResult> DeleteWorkBlock(int workingBlockId)
        {
            WorkingBlock workingBlock = await workingBlockRepository.GetWorkBlock(w => w.Id == workingBlockId);

            if(WorkingBlockLogic.IsBlockDeletable(workingBlock))
            {
                await workingBlockRepository.DeleteWorkBlock(workingBlock);
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        private async Task<bool> CheckIfBlockOverlapsAsync(TimeRange timeRange, int theatreId, int surgeonId)
        {
            bool isOvelaps = false;

            List<WorkingBlock> checkBlocks = null;
            if (!isOvelaps)
            {
                //check whether this time overlaps with any block in the current theatre
                checkBlocks = await workingBlockRepository.GetWorkBlocks(
                            w => w.TheatreId == theatreId &&
                            (w.Start.Date == timeRange.Start.Date || w.End.Date == timeRange.End.Date));

                isOvelaps = WorkingBlockLogic.CheckIfBlockOverlaps(timeRange, checkBlocks);
            }
            else if (!isOvelaps)
            {
                //check whether this time overlaps with any other block of the surgeon
                checkBlocks = await workingBlockRepository.GetWorkBlocks(
                               w => w.SurgeonId == surgeonId &&
                               (w.Start.Date == timeRange.Start.Date || w.End.Date == timeRange.End.Date));
                isOvelaps = WorkingBlockLogic.CheckIfBlockOverlaps(timeRange, checkBlocks);
            }
            return isOvelaps;
        }

    }
}
