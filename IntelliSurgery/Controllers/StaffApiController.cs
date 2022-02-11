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
        private readonly IWorkingBlockLogic workBlockLogic;
        private const string SURGEON_CONFLICT = "Working hours have already been allocted to surgeon at this time";
        private const string THEATRE_CONFLICT = "Theatre is occupied at this time";

        public StaffApiController(IWorkingBlockRepository workingBlockRepository, ISurgeonRepository surgeonRepository,
            ITheatreRepository theatreRepository, IWorkingBlockLogic workBlockLogic)
        {
            this.workingBlockRepository = workingBlockRepository;
            this.surgeonRepository = surgeonRepository;
            this.theatreRepository = theatreRepository;
            this.workBlockLogic = workBlockLogic;
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
            string overLapReason = await CheckIfBlockOverlapsAsync(timeRange, workBlockDTO.TheatreId, surgeon.Id);
            if (overLapReason != null)
            {
                return Json(new { success = false, message = overLapReason });
            }

            //else proceed to create and save the block
            Theatre theatre = await theatreRepository.GetTheatre(TheatreQueryLogic.ById(workBlockDTO.TheatreId));
            
            WorkingBlock workingBlock = new WorkingBlock(surgeon, theatre, timeRange);
            workingBlock = await workingBlockRepository.AddWorkingBlock(workingBlock);
            workingBlock.AllocatedSurgeries = null;//to prevent Json ReferenceLoopHandling.Error 
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
                workingBlock.AllocatedSurgeries = null;//to prevent Json ReferenceLoopHandling.Error 
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

            if(workBlockLogic.IsBlockDeletable(workingBlock))
            {
                await workingBlockRepository.DeleteWorkBlock(workingBlock);
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        private async Task<string> CheckIfBlockOverlapsAsync(TimeRange timeRange, int theatreId, int surgeonId)
        {
            bool isOvelaps = false;
            string reason = null;
            List<WorkingBlock> checkBlocks = null;
            if (!isOvelaps)
            {
                //check whether this time overlaps with any block in the current theatre
                checkBlocks = await workingBlockRepository.GetWorkBlocks(
                            w => w.TheatreId == theatreId &&
                            (w.Start.Date == timeRange.Start.Date || w.End.Date == timeRange.End.Date));

                isOvelaps = workBlockLogic.CheckIfBlockOverlaps(timeRange, checkBlocks);
                reason = isOvelaps ? THEATRE_CONFLICT : null;
            }
            else if (!isOvelaps)
            {
                //check whether this time overlaps with any other block of the surgeon
                checkBlocks = await workingBlockRepository.GetWorkBlocks(
                               w => w.SurgeonId == surgeonId &&
                               (w.Start.Date == timeRange.Start.Date || w.End.Date == timeRange.End.Date));
                isOvelaps = workBlockLogic.CheckIfBlockOverlaps(timeRange, checkBlocks);
                reason = isOvelaps ? SURGEON_CONFLICT : null;
            }
            
            return reason;
        }

    }
}
