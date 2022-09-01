using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.DTOs;

namespace PlanetWars.Services
{
    public interface IPlanetService
    {
        public Task<bool> Add(PlanetDto planetDto);
        public Task<IEnumerable<PlanetDto>> CreatePlanets(CreateGameDto createGameDto, Guid GalaxyID, Guid SessionID);

        public Task<IEnumerable<PlanetDto>> GetAll();
        public Task<PlanetDto> GetById(Guid id);
        public Task<IEnumerable<PlanetDto>> GetForPlayer(Guid playerId);
        public Task<IEnumerable<PlanetDto>> GetRelatedPlanets(Guid planetID);

        public Task<bool> Update(PlanetDto planetDto);

        public Task<bool> Delete(Guid id);
        public Task<bool> DeleteAll();

    }
}