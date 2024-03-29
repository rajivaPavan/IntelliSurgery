﻿using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        private readonly IIncludableQueryable<Appointment, Patient> readAppointments;

        public AppointmentRepository(AppDbContext context)
        {
            this.context = context;
            readAppointments = context.Appointments.Include(a => a.TheatreType)
                                             .Include(a => a.Surgeon)
                                             .Include(a => a.ScheduledSurgery).ThenInclude(s => s.SurgeryEvent)
                                             .Include(a => a.SurgeryType)
                                             .Include(a => a.Theatre)
                                             .Include(a => a.Patient);
        }

        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            await context.Appointments.AddAsync(appointment);
            await context.SaveChangesAsync();
            return appointment;
        }

        public async Task<List<Appointment>> AddAppointments(List<Appointment> appointments)
        {
            await context.Appointments.AddRangeAsync(appointments);
            await context.SaveChangesAsync();
            return appointments;
        }

        public async Task<Appointment> GetAppointment(Expression<Func<Appointment, bool>> expression)
        {
            return await readAppointments.FirstOrDefaultAsync(expression);
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await readAppointments.ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointments(Expression<Func<Appointment, bool>> expression)
        {
            return await readAppointments.Where(expression).ToListAsync();
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

        public async Task DeleteAllAppointments()
        {
            context.Appointments.RemoveRange(await context.Appointments.ToListAsync());
            await context.SaveChangesAsync();
        }
        
    }
}
