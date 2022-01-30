using IntelliSurgery.DbOperations;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.DbOperations.WorkingBlocks;
using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;
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
        private IAppointmentRepository appointmentRepository;


        public CalendarApiController(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetScheduledSurgeries(string filter, int filterValue)
        {
            List<FullCalendarEvent> fullCalendarEvents = new List<FullCalendarEvent>();
            List<Appointment> scheduledAppointments = await appointmentRepository.GetAppointments(a => a.ScheduledSurgery != null);

            if (filter == "theatres")
            {
                scheduledAppointments = scheduledAppointments.FindAll(a => a.Theatre.Id == filterValue);
            }
            else if (filter == "surgeons")
            {
                scheduledAppointments = scheduledAppointments.FindAll(a => a.Surgeon.Id == filterValue);
            }
            else if (filter == "theatreTypes")
            {
                scheduledAppointments = scheduledAppointments.FindAll(a => a.TheatreType.Id == filterValue);

            }
            else if (filter == "surgeryTypes")
            {
                scheduledAppointments = scheduledAppointments.FindAll(a => a.SurgeryType.Id == filterValue);
            }

            foreach (var appointment in scheduledAppointments)
            {
                fullCalendarEvents.Add(new FullCalendarEvent() { 
                    Id = filter+filterValue.ToString()+appointment.Id.ToString(),
                    Title = appointment.Patient.Name + " : "+appointment.SurgeryType.Name,
                    Start = appointment.ScheduledSurgery.SurgeryEvent.Start,
                    End = appointment.ScheduledSurgery.SurgeryEvent.End,
                    ExtendedProps = appointment,
                    Color = FullCalendarEvent.GetPriorityColor(appointment.PriorityLevel)
                });
            }
            //hardcoded values
            fullCalendarEvents.Add(new FullCalendarEvent()
            {
                Id = "surgeons-1-1",
                Title = "Patient Name: Surgery",
                Start = DateTime.Now,
                End = DateTime.Now.Add(new TimeSpan(1,0,0)),
                ExtendedProps = await appointmentRepository.GetAppointment(a => a.Id==1),
                Color = FullCalendarEvent.GetPriorityColor(PriorityLevel.Low)
            });
            fullCalendarEvents.Add(new FullCalendarEvent()
            {
                Id = "surgeons-2-2",
                Title = "Patient Name: Surgery",
                Start = DateTime.Now.Add(new TimeSpan(1, 0, 0)),
                End = DateTime.Now.Add(new TimeSpan(2, 0, 0)),
                ExtendedProps = await appointmentRepository.GetAppointment(a => a.Id == 2),
                Color = FullCalendarEvent.GetPriorityColor(PriorityLevel.High)
            });
            fullCalendarEvents.Add(new FullCalendarEvent()
            {
                Id = "surgeons-1-3",
                Title = "Patient Name: Surgery",
                Start = DateTime.Now.Add(new TimeSpan(2, 20, 0)),
                End = DateTime.Now.Add(new TimeSpan(3, 0, 0)),
                ExtendedProps = await appointmentRepository.GetAppointment(a => a.Id == 3),
                Color = FullCalendarEvent.GetPriorityColor(PriorityLevel.Medium)
            });

            return Json(new { success = true, data = fullCalendarEvents });
        }
    }

    public class FullCalendarEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Appointment ExtendedProps { get; set; }
        public string Color { get; set; }

        public static string GetPriorityColor(PriorityLevel priorityLevel)
        {
            string highColor = "#ff8f8f";
            string lowColor = "#94ff8f";
            string mediumColor = "#ffe18f";
            string color;
            if(priorityLevel == PriorityLevel.Low)
            {
                color = lowColor;
            }
            else if(priorityLevel == PriorityLevel.Medium)
            {
                color = mediumColor;
            }
            else 
            {
                color = highColor;
            }
            return color;
        }
    }
}
