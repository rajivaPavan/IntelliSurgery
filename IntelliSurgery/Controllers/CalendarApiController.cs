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


        public CalendarApiController(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetScheduledSurgeries(string filter, int filterValue)
        {
            List<AppointmentCalendarEvent> fullCalendarEvents = new List<AppointmentCalendarEvent>();
            List<Appointment> scheduledAppointments = new List<Appointment>();

            if (filter == "theatres")
            {
                scheduledAppointments = await appointmentRepository.GetAppointments(a => a.ScheduledSurgery != null && a.Theatre.Id == filterValue);
            }
            else if (filter == "surgeons")
            {
                scheduledAppointments = await appointmentRepository.GetAppointments(a => a.ScheduledSurgery != null && a.Surgeon.Id == filterValue);
            }
            else if (filter == "theatreTypes")
            {
                scheduledAppointments = await appointmentRepository.GetAppointments(a => a.ScheduledSurgery != null && a.TheatreType.Id == filterValue);

            }
            else if (filter == "surgeryTypes")
            {
                scheduledAppointments = await appointmentRepository.GetAppointments(a => a.ScheduledSurgery != null && a.SurgeryType.Id == filterValue);
            }

            foreach (var appointment in scheduledAppointments)
            {
                fullCalendarEvents.Add(new AppointmentCalendarEvent() { 
                    Id = filter+filterValue.ToString()+appointment.Id.ToString(),
                    Title = appointment.Patient.Name + " : "+appointment.SurgeryType.Name,
                    Start = appointment.ScheduledSurgery.SurgeryEvent.Start,
                    End = appointment.ScheduledSurgery.SurgeryEvent.End,
                    ExtendedProps = appointment,
                    Color = AppointmentCalendarEvent.GetPriorityColor(appointment.PriorityLevel)
                });
            }
            //hardcoded values
            fullCalendarEvents.Add(new AppointmentCalendarEvent()
            {
                Id = "surgeons-1-1",
                Title = "Patient Name: Surgery",
                Start = DateTime.Now,
                End = DateTime.Now.Add(new TimeSpan(1,0,0)),
                ExtendedProps = await appointmentRepository.GetAppointment(a => a.Id==1),
                Color = AppointmentCalendarEvent.GetPriorityColor(PriorityLevel.Low)
            });
            fullCalendarEvents.Add(new AppointmentCalendarEvent()
            {
                Id = "surgeons-2-2",
                Title = "Patient Name: Surgery",
                Start = DateTime.Now.Add(new TimeSpan(1, 0, 0)),
                End = DateTime.Now.Add(new TimeSpan(2, 0, 0)),
                ExtendedProps = await appointmentRepository.GetAppointment(a => a.Id == 2),
                Color = AppointmentCalendarEvent.GetPriorityColor(PriorityLevel.High)
            });
            fullCalendarEvents.Add(new AppointmentCalendarEvent()
            {
                Id = "surgeons-1-3",
                Title = "Patient Name: Surgery",
                Start = DateTime.Now.Add(new TimeSpan(2, 20, 0)),
                End = DateTime.Now.Add(new TimeSpan(3, 0, 0)),
                ExtendedProps = await appointmentRepository.GetAppointment(a => a.Id == 3),
                Color = AppointmentCalendarEvent.GetPriorityColor(PriorityLevel.Medium)
            });

            return Json(new { success = true, data = fullCalendarEvents });
        }
    }

    
}
