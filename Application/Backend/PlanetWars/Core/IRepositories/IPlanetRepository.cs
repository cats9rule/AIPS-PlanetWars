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
        public Task<bool> UpdateArmies(Guid id, int armyDifference);
        public Task<bool> UpdateOwnership(Guid id, Guid ownerID);
        public Task<bool> UpdateOwnership(int planetIndex, Guid galaxyID, Guid ownerID);
        public Task<bool> DeleteAll();  
    }
}