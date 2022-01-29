using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations.Theatres
{
    public class TheatreRepository : ITheatreRepository
    {
        private readonly AppDbContext context;

        public TheatreRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<TheatreType> GetTheatreType(Expression<Func<TheatreType,bool>> expression)
        {
            return await context.TheatreTypes.FirstOrDefaultAsync(expression);

        }

        public async Task<List<Theatre>> GetTheatres(Expression<Func<Theatre, bool>> expression)
        {
            return await context.Theatres.Where(expression).ToListAsync();
        }

        public async Task<List<TheatreType>> GetAllTheatreTypes()
        {
            return await context.TheatreTypes.ToListAsync();
        }
    }

    public class TheatreQueryLogic
    {
        public static Expression<Func<Theatre, bool>> ById(int id)
        {
            return t => t.Id == id;
        }
        public static Expression<Func<Theatre,bool>> ByTheatreType(TheatreType theatreType)
        {
            return t => t.TheatreType == theatreType;
        }
    }

    public static class TheatreTypeQueryLogic
    {
        public static Expression<Func<TheatreType,bool>> ById(int id)
        {
            return t => t.Id == id;
        }
    }
}
