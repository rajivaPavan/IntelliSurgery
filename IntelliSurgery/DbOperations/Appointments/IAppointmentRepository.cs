using IntelliSurgery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetAppointmentById(int id);
        Task<List<Appointment>> GetAppointments();
        Task<Appointment> CreateAppointment(Appointment appointment);
    }
}
