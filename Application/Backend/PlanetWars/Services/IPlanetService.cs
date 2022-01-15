using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.DTOs;

namespace PlanetWars.Services
{
    public interface IPlanetService
    {
        public Task<bool> Add(PlanetDto planetDto);
        public Task<IEnumerable<PlanetDto>> CreatePlanets(int planetCount, bool hasResource);

        public Task<IEnumerable<PlanetDto>> GetAll();
        public Task<PlanetDto> GetById(Guid id);
        public Task<IEnumerable<PlanetDto>> GetForPlayer(Guid playerId);
        public Task<IEnumerable<PlanetDto>> GetRelatedPlanets(Guid planetID);

        public Task<bool> Update(PlanetDto planetDto);
        public Task<bool> UpdateArmies(Guid id, int armyDifference);
        public Task<bool> UpdateOwnership(Guid id, Guid ownerID);

        public Task<bool> Delete(Guid id);
    }
}