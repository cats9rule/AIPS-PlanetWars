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
    public class GameMapRepository : GenericRepository<GameMap>, IGameMapRepository
    {
        public GameMapRepository(PlanetWarsDbContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<GameMap>> GetAll() 
        {
            return await base.GetAll();
        }

        public async Task<IEnumerable<GameMap>> GetByPlanetCount(int planetCount)
        {
            return await dbSet.Where(gm => gm.PlanetCount == planetCount).ToListAsync();
        }

        public async Task<IEnumerable<GameMap>> GetByResourcePlanetRatio(float ratio)
        {
            return await dbSet.Where(gm => gm.ResourcePlanetRatio == ratio).ToListAsync();
        }

        public override async Task<bool> Add(GameMap entity)
        {
            await dbSet.AddAsync(entity);
            return true;
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

        public override async Task<bool> Update(GameMap entity)
        {
            var exist = await dbSet.Where(x => x.ID == entity.ID).FirstOrDefaultAsync();
            if (exist == null)
                return await Add(entity);
            dbSet.Update(entity);
            return true;
        }
    }
}