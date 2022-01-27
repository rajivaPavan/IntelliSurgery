﻿using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System;
using System.Linq.Expressions;

namespace IntelliSurgery.DbOperations.Appointments
{
    public static class AppointmentQueryLogic
    {
        public static Expression<Func<Appointment, bool>> ById(int appointmentId)
        {
            return a => a.Id == appointmentId;
        }


        public static Expression<Func<Appointment, bool>> ByPriorityLevel(PriorityLevel priorityLevel)
        {
            return a => a.PriorityLevel == priorityLevel;
        }
    }
}