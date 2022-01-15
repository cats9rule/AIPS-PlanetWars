using PlanetWars.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetWars.Services
{
    public interface IGalaxyService
    {
        public Task<GalaxyDto> CreateGalaxy(int planetCount, float resourcePlanetRatio);
        public Task<IEnumerable<GalaxyDto>> GetAllGalaxies();
        public Task<GalaxyDto> GetGalaxy(Guid id);
        public Task<IEnumerable<GalaxyDto>> GetGalaxiesByPlanetCount(int count);
        public Task<bool> UpdateGalaxy(GalaxyDto dto);
        public Task<bool> DeleteGalaxy(Guid id);
    }
}