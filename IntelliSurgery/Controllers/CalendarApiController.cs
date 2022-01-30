using IntelliSurgery.DbOperations;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.DbOperations.WorkingBlocks;
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
    public class CalendarApiController : Controller
    {
        private readonly IWorkingBlockRepository workingBlockRepository;
        private readonly ITheatreRepository theatreRepository;

        public CalendarApiController(IWorkingBlockRepository workingBlockRepository)
        {
            this.workingBlockRepository = workingBlockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetScheduledSurgeries(int theatreId)
        {
            Theatre theatre = await theatreRepository.GetTheatre(TheatreQueryLogic.ById(theatreId));
            List<Appointment> scheduledAppointments = theatre.ScheduledAppointments;
            List<FullCalendarEvent> fullCalendarEvents = new List<FullCalendarEvent>();
            foreach(var appointment in scheduledAppointments)
            {
                fullCalendarEvents.Add(new FullCalendarEvent() { 
                    Title = appointment.Surgeon.Name+" : "+appointment.Patient.Name,
                    Start = appointment.ScheduledSurgery.SurgeryEvent.Start,
                    End = appointment.ScheduledSurgery.SurgeryEvent.End,
                    Appointment = appointment
                });
            }
            return Json(new { success = true, data = fullCalendarEvents });
        }
    }

    public class FullCalendarEvent
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Appointment Appointment { get; set; }
    }
}
