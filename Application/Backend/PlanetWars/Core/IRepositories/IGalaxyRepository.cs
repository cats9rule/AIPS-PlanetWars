using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.Data.Models;

namespace PlanetWars.Core.IRepositories
{
    public interface IGalaxyRepository : IGenericRepository<Galaxy>
    {
        public Task<IEnumerable<Galaxy>> GetGalaxiesByResourceAbundanceFactor(float abundanceFactor);
        public Task<List<Galaxy>> GetGalaxiesByPlanetCount(int planetCount);
    }
}