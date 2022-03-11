using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services
{
    public interface IGameMapService
    {
        public Task<bool> AddGameMap(GameMap gm);
        public Task<IEnumerable<GameMapDto>> GetAll();
        public Task<IEnumerable<GameMap>> GetByPlanetCount(int planetCount);
        public Task<IEnumerable<GameMap>> GetByResourcePlanetRatio(float ratio);
    }
}