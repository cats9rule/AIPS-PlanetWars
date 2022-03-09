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
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        public SessionRepository(PlanetWarsDbContext context) : base(context)
        {

        }

        public override Task<bool> Add(Session entity)
        {
            return base.Add(entity);
        }

        public async override Task<IEnumerable<Session>> GetAll()
        {
            return await dbSet.Include(p => p.Galaxy).Include(p => p.Players).ToListAsync();
        }

        public override Task<Session> GetById(Guid sessionId)
        {
            return base.GetById(sessionId);
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

        public override async Task<bool> Update(Session entity)
        {
            var exist = await dbSet.Where(x => x.ID == entity.ID).FirstOrDefaultAsync();
            if (exist == null)
                return await Add(entity);
            dbSet.Update(entity);
            return true;
        }


        
        public async Task<IEnumerable<Session>> GetByName(string name)
        {
            var exist = await dbSet.Where(x => x.Name == name).ToListAsync();
            return exist;   // if (exist == null) return exist;
        }
    }
}