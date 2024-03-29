﻿using IntelliSurgery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface ISurgeonRepository
    {
        Task<Surgeon> GetSurgeonById(int id);
        Task<List<Surgeon>> GetAllSurgeons();
        Task<Surgeon> AddSurgeon(Surgeon surgeon);
        Task<List<Surgeon>> AddSurgeons(List<Surgeon> surgeons);
        Task DeleteSurgeons(List<Surgeon> surgeons);
    }
}
