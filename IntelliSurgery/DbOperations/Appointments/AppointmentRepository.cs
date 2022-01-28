using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<Appointment> GetAppointment(Expression<Func<Appointment, bool>> predicate)
        {
            return await context.Appointments.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await context.Appointments.ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointments(Expression<Func<Appointment, bool>> predicate)
        {
            return await context.Appointments.Where(predicate).ToListAsync();
        }

        public async Task<List<Appointment>> UpdateAppointments(List<Appointment> appointments)
        {
            context.Appointments.UpdateRange(appointments);
            await context.SaveChangesAsync();
            return appointments;
        }

        public async Task<Appointment> UpdateAppointment(Appointment appointment)
        {
            context.Appointments.Update(appointment);
            await context.SaveChangesAsync();
            return appointment;
        }
    }
}
