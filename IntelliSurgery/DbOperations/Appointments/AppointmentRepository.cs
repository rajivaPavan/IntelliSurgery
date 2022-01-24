using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            await context.Appointments.AddAsync(appointment);
            await context.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
            return await context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            return await context.Appointments.ToListAsync();
        }
    }
}
