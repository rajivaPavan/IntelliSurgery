using IntelliSurgery.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations.Theatres
{
    public interface ITheatreRepository
    {
        Task<TheatreType> GetTheatreType(Expression<Func<TheatreType,bool>> expression);
        Task<List<Theatre>> GetTheatres(Expression<Func<Theatre, bool>> expression);
    }
}