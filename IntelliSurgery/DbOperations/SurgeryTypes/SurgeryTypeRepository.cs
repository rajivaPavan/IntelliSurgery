﻿using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public class SurgeryTypeRepository : ISurgeryTypeRepository
    {
        private readonly AppDbContext context;

        public SurgeryTypeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<SurgeryType> AddSurgeryType(SurgeryType surgery)
        {
            await context.SurgeryTypes.AddAsync(surgery);
            await context.SaveChangesAsync();
            return surgery;
        }

        public async Task<SurgeryType> GetSurgeryTypeById(int id)
        {
            return await context.SurgeryTypes.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SurgeryType>> GetAllSurgeryTypes()
        {
            return await context.SurgeryTypes.ToListAsync();
        }
    }
}
