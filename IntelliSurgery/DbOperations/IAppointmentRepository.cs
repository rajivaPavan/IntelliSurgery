using IntelliSurgery.Models;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetAppointmentById(int id);
        Task<Appointment> CreateAppointment(Appointment appointment);
    }
}
