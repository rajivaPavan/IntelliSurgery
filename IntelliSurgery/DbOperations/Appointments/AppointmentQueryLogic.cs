using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System;
using System.Linq.Expressions;
using static IntelliSurgery.Enums.OperationTheatreEnums;

namespace IntelliSurgery.DbOperations.Appointments
{
    public static class AppointmentQueryLogic
    {
        public static Expression<Func<Appointment, bool>> ById(int appointmentId)
        {
            return a => a.Id == appointmentId;
        }
        public static Expression<Func<Appointment, bool>> ByTheatreType(TheatreType theatreType)
        {
            return a => a.TheatreType == theatreType;
        }

        public static Expression<Func<Appointment, bool>> ByPriorityLevel(PriorityLevel priorityLevel)
        {
            return a => a.PriorityLevel == priorityLevel;
        }

        public static Expression<Func<Appointment, bool>> AfterSpecificDate(DateTime dateTime)
        {
            return a => a.DateAdded >= dateTime;
        }
        public static Expression<Func<Appointment, bool>> StatusNotEqual(Status status)
        {
            return a => a.Status != status;
        }
    }
}
