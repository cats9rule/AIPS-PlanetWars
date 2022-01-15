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
    public class PlayerColorRepository : GenericRepository<PlayerColor>, IPlayerColorRepository
    {
        public PlayerColorRepository(PlanetWarsDbContext context) : base(context)
        {
            
        }

        public override Task<bool> Add(PlayerColor entity)
        {
            return base.Add(entity);
        }

        public override Task<IEnumerable<PlayerColor>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<PlayerColor> GetById(Guid id)
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

        public override async Task<bool> Update(PlayerColor entity)
        {
            var existing = await dbSet.Where(x => x.ID == entity.ID).FirstOrDefaultAsync();
            if (existing == null)
            {
                return await Add(entity);
            }
            dbSet.Update(entity);
            return true;
        }

        public async Task<PlayerColor> GetByTurnIndex(int turnIndex)
        {
            return await dbSet.Where(x => x.TurnIndex == turnIndex).FirstOrDefaultAsync();
        }
    }
}