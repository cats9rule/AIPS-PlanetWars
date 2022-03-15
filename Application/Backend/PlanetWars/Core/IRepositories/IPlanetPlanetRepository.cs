using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.Data.Models;

namespace PlanetWars.Core.IRepositories
{
    public interface IPlanetPlanetRepository : IGenericRepository<PlanetPlanet>
    {
        public Task<IEnumerable<PlanetPlanet>> GetAllRelationsForPlanet(Guid planetID);
        public Task<bool> DeleteAll();

        public Task<IEnumerable<PlanetPlanet>> DeleteAllRelationsForPlanet(Guid planetID);

        public Task<bool> AddAll(List<PlanetPlanet> relations);
    }
}