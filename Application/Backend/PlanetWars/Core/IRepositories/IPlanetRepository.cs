using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.Data.Models;

namespace PlanetWars.Core.IRepositories
{
    public interface IPlanetRepository: IGenericRepository<Planet>
    {
        public Task<IEnumerable<Planet>> GetForPlayer(Guid playerID);
        public Task<IEnumerable<Planet>> GetForSession(Guid sessionID);
        public Task<bool> DeleteAll(); 
        public Task<bool> DeleteForSession(Guid sessionID);

    }
}