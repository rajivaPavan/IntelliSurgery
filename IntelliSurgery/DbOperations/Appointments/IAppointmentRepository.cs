using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface IAppointmentRepository
    { 
        Task<List<Appointment>> GetAllAppointments();
        Task<Appointment> AddAppointment(Appointment appointment);
        Task<List<Appointment>> AddAppointments(List<Appointment> appointments);
        Task<List<Appointment>> UpdateAppointments(List<Appointment> appointments);
        Task<List<Appointment>> GetAppointments(Expression<Func<Appointment, bool>> expression);
        Task<Appointment> GetAppointment(Expression<Func<Appointment, bool>> expression);
        Task<Appointment> UpdateAppointment(Appointment appointment);
    }
}
