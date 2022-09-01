using PlanetWars.Core.IRepositories;
using PlanetWars.Data.Models;
using PlanetWars.Data.Context;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace PlanetWars.Core.Repositories
{
    public class GalaxyRepository : GenericRepository<Galaxy>, IGalaxyRepository
    {

        public GalaxyRepository(PlanetWarsDbContext context) : base(context)
        {

        }

        public override Task<bool> Add(Galaxy entity)
        {
            return base.Add(entity);
        }

        public override Task<IEnumerable<Galaxy>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<Galaxy> GetById(Guid id)
        {
            return base.GetById(id);
        }

        public override async Task<bool> Delete(Guid id)
        {
            var exist = await dbSet.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (exist != null)
            {
                dbSet.Remove(exist);
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteForSession(Guid sessionID) 
        {
            var galaxy = await dbSet.Where(g => g.SessionID == sessionID).FirstOrDefaultAsync();
            dbSet.Remove(galaxy);
            return true;
        }

        public override async Task<bool> Update(Galaxy entity)
        {
            var exist = await dbSet.Where(x => x.ID == entity.ID).FirstOrDefaultAsync();
            if (exist == null)
                return await Add(entity);
            dbSet.Update(entity);
            return true;
        }

        public async Task<IEnumerable<Galaxy>> GetGalaxiesByResourceAbundanceFactor(float abundanceFactor)
        {
            return await dbSet.Where(g => g.ResourcePlanetRatio == abundanceFactor).ToListAsync();
        }

        public async Task<List<Galaxy>> GetGalaxiesByPlanetCount(int planetCount)
        {
            return await dbSet.Where(g => g.PlanetCount == planetCount).ToListAsync();
        }
    }
}