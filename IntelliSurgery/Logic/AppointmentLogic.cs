using IntelliSurgery.DbOperations;
using IntelliSurgery.Models;
using System.Threading.Tasks;

namespace IntelliSurgery.Logic
{
    public interface IAppointmentLogic
    {
        Task<Appointment> DeleteScheduledSurgeryAsync(Appointment appointment);
    }

    public class AppointmentLogic : IAppointmentLogic
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly ISurgeryRepository surgeryRepository;

        public AppointmentLogic(IAppointmentRepository appointmentRepository, ISurgeryRepository surgeryRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.surgeryRepository = surgeryRepository;
        }
        public async Task<Appointment> DeleteScheduledSurgeryAsync(Appointment appointment)
        {
            //delete relevant appointment scheduled surgery and surgery event

            SurgeryEvent delSurgeryEvent = appointment.ScheduledSurgery.SurgeryEvent;
            ScheduledSurgery delScheduledSurgery = appointment.ScheduledSurgery;

            //delete scheduled surgery
            appointment.ScheduledSurgeryId = null;
            appointment.ScheduledSurgery = null;

            //set prev allocated theatre to null
            appointment.Theatre = null;
            appointment.TheatreId = null;

            //sever appointment from workblock
            appointment.WorkingBlock = null;
            appointment.WorkingBlockId = null;

            await appointmentRepository.UpdateAppointment(appointment);
            await surgeryRepository.DeleteScheduleSurgery(delScheduledSurgery);
            await surgeryRepository.DeleteSurgeryEvent(delSurgeryEvent);
            return appointment;
        }
    }


}
