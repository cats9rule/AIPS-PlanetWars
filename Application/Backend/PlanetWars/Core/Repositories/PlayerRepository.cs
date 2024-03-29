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

        public async Task<bool> DeleteForSession(Guid sessionID) 
        {
            var players = await dbSet.Where(p => p.SessionID == sessionID).ToListAsync();
            dbSet.RemoveRange(players);
            return true;
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

        public async Task<IEnumerable<Player>> GetByUsernameAndTag(string username, string tag)
        {
            return await dbSet.Where(player => player.User.Username == username && player.User.Tag == tag).ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetByUserId(Guid id)
        {
            return await dbSet.Where(player => player.User.ID == id).ToListAsync();
        }
    }
}