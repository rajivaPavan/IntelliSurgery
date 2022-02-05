using IntelliSurgery.DTOs;
using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        private IIncludableQueryable<TheatreType, List<SurgeryType>> readTheatreTypes;
        private IIncludableQueryable<Theatre, TheatreType> readTheatres;

        public TheatreRepository(AppDbContext context)
        {
            this.context = context;
            this.readTheatreTypes = context.TheatreTypes.Include(t => t.SurgeryTypesConducted);
            this.readTheatres = context.Theatres.Include(t => t.TheatreType);
        }
        public async Task<TheatreType> GetTheatreType(Expression<Func<TheatreType,bool>> expression)
        {
            return await readTheatreTypes.FirstOrDefaultAsync(expression);
        }

        public async Task<List<Theatre>> GetTheatres(Expression<Func<Theatre, bool>> expression)
        {
            return await readTheatres.Where(expression).ToListAsync();
        }

        public async Task<Theatre> GetTheatre(Expression<Func<Theatre, bool>> expression)
        {
            return await readTheatres.FirstOrDefaultAsync(expression);
        }

        public async Task<List<TheatreType>> GetAllTheatreTypes()
        {
            return await readTheatreTypes.ToListAsync();
        }

        public async Task<List<Theatre>> AddTheatres(List<Theatre> theatres)
        {
            await context.Theatres.AddRangeAsync(theatres);
            await context.SaveChangesAsync();
            return theatres;
        }

        public async Task<List<TheatreType>> AddTheatreTypes(List<TheatreType> theatreTypes)
        {
            await context.TheatreTypes.AddRangeAsync(theatreTypes);
            await context.SaveChangesAsync();
            return theatreTypes;
        }

        public async Task<List<Theatre>> GetAllTheatres()
        {
            return await readTheatres.ToListAsync();
        }

        public Task<List<TheatreType>> DeleteTheatreTypes(List<TheatreType> theatreTypes)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTheatres(List<Theatre> theatres)
        {
            throw new NotImplementedException();
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
