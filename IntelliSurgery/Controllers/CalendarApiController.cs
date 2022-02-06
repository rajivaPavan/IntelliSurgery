using IntelliSurgery.DbOperations;
using IntelliSurgery.DbOperations.Appointments;
using IntelliSurgery.DbOperations.Theatres;
using IntelliSurgery.DbOperations.WorkingBlocks;
using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;
using IntelliSurgery.Logic;
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
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IWorkingBlockRepository workingBlockRepository;
        private readonly IAppointmentLogic appointmentLogic;
        private readonly IWorkingBlockLogic workBlockLogic;

        public CalendarApiController(IAppointmentRepository appointmentRepository,IWorkingBlockRepository workingBlockRepository,
            IAppointmentLogic appointmentLogic, IWorkingBlockLogic workBlockLogic)
        {
            this.appointmentRepository = appointmentRepository;
            this.workingBlockRepository = workingBlockRepository;
            this.appointmentLogic = appointmentLogic;
            this.workBlockLogic = workBlockLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetScheduledSurgeries(string filter, int filterValue)
        {
            List<AppointmentCalendarEvent> fullCalendarEvents = new List<AppointmentCalendarEvent>();
            List<Appointment> scheduledAppointments = new List<Appointment>();

            if (filter == "theatres")
            {
                scheduledAppointments = await appointmentRepository.GetAppointments(a => a.ScheduledSurgeryId != null && a.TheatreId == filterValue);
                List<WorkingBlock> workingBlocks = await workingBlockRepository.GetWorkBlocks(w => w.TheatreId == filterValue);
                foreach (WorkingBlock workingBlock in workingBlocks)
                {
                    fullCalendarEvents.Add(new AppointmentCalendarEvent(workingBlock));
                }
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

        [HttpPost]
        public async Task<IActionResult> GetCalendarEvent(int appointmentId)
        {
            Appointment appointment = await appointmentRepository.GetAppointment(AppointmentQueryLogic.ById(appointmentId));
            if (appointment != null)
            {
                AppointmentCalendarEvent calendarEvent = new AppointmentCalendarEvent(appointment);
                return Json(new { success = true, data = calendarEvent });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAppointmentStatus(int appointmentId, int newStatus)
        {
            string errorMessage = "";
            Appointment appointment = await appointmentRepository.GetAppointment(AppointmentQueryLogic.ById(appointmentId));
            if (appointment != null)
            {
                bool isChangeStatus = true;

                Status appointmentStatus = (Status)newStatus;
                if(appointmentStatus == Status.Postponed && appointment.ScheduledSurgery == null && appointment.Status != Status.Postponed)
                {
                    //change status back to pending since if previous status was not postponed and no surgery was allocated
                    appointmentStatus = Status.Pending; 
                }
                else if( (appointmentStatus == Status.Cancelled || appointmentStatus == Status.Postponed) 
                    && appointment.ScheduledSurgery != null )
                {
                    //restores the workblock time
                    workBlockLogic.RestoreWorkBlockTime(appointment).Wait();

                    //delete relevant appointment scheduled surgery and surgery event
                    await appointmentLogic.DeleteScheduledSurgeryAsync(appointment);
                    
                }else if(appointmentStatus == Status.Ongoing)
                {
                    DateTime now = DateTime.Now;
                    if (!(appointment.ScheduledSurgery.SurgeryEvent.Start <= now
                        && now <= appointment.ScheduledSurgery.SurgeryEvent.End))
                    {
                        isChangeStatus = false;
                        errorMessage = "Surgery status cannot be set to ongoing";
                    }
                }
                else if(appointmentStatus == Status.Completed)
                {
                    if(appointment.ScheduledSurgery.SurgeryEvent.End > DateTime.Now)
                    {
                        isChangeStatus = false;
                        errorMessage = "Surgery status cannot be set to completed";
                    }
                }

                if (isChangeStatus)
                {
                    appointment.Status = appointmentStatus;
                    appointment = await appointmentRepository.UpdateAppointment(appointment);
                    AppointmentExtendedProp appointmentExtendedProp = new AppointmentExtendedProp(appointment);
                    return Json(new { success = true, data = appointmentExtendedProp });
                }

                return Json(new { success = false, error = errorMessage });
                
            }

            return Json(new { success = false, error = errorMessage });
        }
    }

    
}
