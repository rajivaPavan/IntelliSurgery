using IntelliSurgery.Models;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext context;

        public AppointmentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Task<Appointment> CreateAppointment(Appointment appointment)
        {
            throw new System.NotImplementedException();
        }

        public Task<Appointment> GetAppointmentById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
