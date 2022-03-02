using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.Data.Models;

namespace PlanetWars.Services
{
    public interface IGameMapService
    {
        public Task<bool> AddGameMap(GameMap gm);
        public Task<IEnumerable<GameMap>> GetAll();
        public Task<IEnumerable<GameMap>> GetByPlanetCount(int planetCount);
        public Task<IEnumerable<GameMap>> GetByResourcePlanetRatio(float ratio);
    }
}