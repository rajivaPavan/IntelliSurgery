using IntelliSurgery.DbOperations;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.DbOperations.WorkingBlocks;
using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntelliSurgery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CalendarApiController : Controller
    {
        private IAppointmentRepository appointmentRepository;
        private readonly IWorkingBlockRepository workingBlockRepository;

        public CalendarApiController(IAppointmentRepository appointmentRepository,IWorkingBlockRepository workingBlockRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.workingBlockRepository = workingBlockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetScheduledSurgeries(string filter, int filterValue)
        {
            List<AppointmentCalendarEvent> fullCalendarEvents = new List<AppointmentCalendarEvent>();
            List<Appointment> scheduledAppointments = new List<Appointment>();

            if (filter == "theatres")
            {
                scheduledAppointments = await appointmentRepository.GetAppointments(a => a.ScheduledSurgeryId != null && a.TheatreId == filterValue);
            }
            else if (filter == "surgeons")
            {
                scheduledAppointments = await appointmentRepository.GetAppointments(a => a.ScheduledSurgeryId != null && a.SurgeonId == filterValue);
                List<WorkingBlock> workingBlocks = await workingBlockRepository.GetWorkBlocks(w => w.SurgeonId == filterValue);
                foreach(WorkingBlock workingBlock in workingBlocks)
                {
                    fullCalendarEvents.Add(new AppointmentCalendarEvent(workingBlock));
                }

            }
            else if (filter == "theatreTypes")
            {
                scheduledAppointments = await appointmentRepository.GetAppointments(a => a.ScheduledSurgeryId != null && a.TheatreTypeId == filterValue);

            }
            else if (filter == "surgeryTypes")
            {
                scheduledAppointments = await appointmentRepository.GetAppointments(a => a.ScheduledSurgeryId != null && a.SurgeryTypeId == filterValue);
            }

            foreach (var appointment in scheduledAppointments)
            {
                AppointmentCalendarEvent calendarEvent = new AppointmentCalendarEvent(appointment);
                calendarEvent.Id = filter + filterValue.ToString() + appointment.Id.ToString();
                fullCalendarEvents.Add(calendarEvent);
            }

            return Json(new { success = true, data = fullCalendarEvents });
        }
    }

    
}
