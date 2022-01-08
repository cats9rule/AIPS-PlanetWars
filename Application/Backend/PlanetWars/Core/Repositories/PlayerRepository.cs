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
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(PlanetWarsDbContext context) : base(context)
        {
            
        }

        public override Task<bool> Add(Player entity)
        {
            return base.Add(entity);
        }

        public override Task<IEnumerable<Player>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<Player> GetById(Guid id)
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

        public override async Task<bool> Update(Player entity)
        {
            var existing = await dbSet.Where(x => x.ID == entity.ID).FirstOrDefaultAsync();
            if (existing == null)
            {
                return await Add(entity);
            }
            dbSet.Update(entity);
            return true;
        }

        public async Task<Player> GetByUsernameAndTag(string username, string tag)
        {
            return await this.dbSet.FirstOrDefaultAsync(player => player.User.Username == username && player.User.Tag == tag);
        }

        public async Task<Player> GetByUserID(Guid id)
        {
            return await this.dbSet.FirstOrDefaultAsync(player => player.User.ID == id);
        }
    }
}