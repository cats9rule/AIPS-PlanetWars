using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.Data.Models;

namespace PlanetWars.Core.IRepositories
{
    public interface IGameMapRepository : IGenericRepository<GameMap>
    {
        public Task<IEnumerable<GameMap>> GetByPlanetCount(int planetCount);
        public Task<IEnumerable<GameMap>> GetByResourcePlanetRatio(float ratio);
    }
}