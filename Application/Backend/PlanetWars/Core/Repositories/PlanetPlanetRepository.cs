using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlanetWars.Core.IRepositories;
using PlanetWars.Data.Context;
using PlanetWars.Data.Models;

namespace PlanetWars.Core.Repositories
{
    public class PlanetPlanetRepository : GenericRepository<PlanetPlanet>, IPlanetPlanetRepository
    {
        public PlanetPlanetRepository(PlanetWarsDbContext context) : base(context)
        {

        }

        public override Task<bool> Add(PlanetPlanet entity)
        {
            return base.Add(entity);
        }

        public override Task<IEnumerable<PlanetPlanet>> GetAll()
        {
            return base.GetAll();
        }

        public async Task<PlanetPlanet> GetById(Guid idPlanetFrom, Guid idPlanetTo)
        {
            return await dbSet.FindAsync(idPlanetFrom, idPlanetTo);
        }

        public async Task<bool> Delete(Guid planetFromID, Guid planetToID)
        {
            var exist = await dbSet.FindAsync(planetFromID, planetToID);
            if(exist != null)
            {
                dbSet.Remove(exist);
                return true;
            }
            return false;
        }

        public override async Task<bool> Update(PlanetPlanet entity)
        {
            var exist = await dbSet.FindAsync(entity.ID);
            if(exist == null)
                return await Add(entity);
            dbSet.Update(entity);
            return true;
        }

        public async Task<IEnumerable<PlanetPlanet>> GetAllRelationsForPlanet(Guid planetID)
        {
            return await dbSet.Where(pp => pp.PlanetFromID == planetID).ToListAsync();

        }

        public async Task<bool> DeleteAll()
        {
            dbSet.RemoveRange(await dbSet.ToArrayAsync());
            return true;
        }

        public async Task<IEnumerable<PlanetPlanet>> DeleteAllRelationsForPlanet(Guid planetID)
        {
            List<PlanetPlanet> relationsToDelete = await dbSet.Where(relation => relation.PlanetFromID == planetID).ToListAsync();
            dbSet.RemoveRange(relationsToDelete);
            return relationsToDelete;
        }

        public async Task<bool> AddAll(List<PlanetPlanet> relations)
        {
            await dbSet.AddRangeAsync(relations);
            return true;
        }
    }
}