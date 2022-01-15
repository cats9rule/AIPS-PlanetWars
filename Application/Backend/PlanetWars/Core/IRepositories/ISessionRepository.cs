using PlanetWars.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace PlanetWars.Core.IRepositories
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        public Task<IEnumerable<Session>> GetByName(string name);
    }
}