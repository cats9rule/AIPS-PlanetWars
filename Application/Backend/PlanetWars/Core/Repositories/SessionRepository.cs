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
            return await dbSet.Include(s => s.Galaxy).ThenInclude(g => g.GameMap)
            .Include(s => s.Players).ThenInclude(p => p.User)
            .Include(s => s.Players).ThenInclude(p => p.PlayerColor)
            .Include(s => s.Players).ThenInclude(p => p.Planets)
            .Include(a => a.Galaxy).ThenInclude(g => g.Planets).ToListAsync();
        }

        public async override Task<Session> GetById(Guid sessionId)
        {
            //return base.GetById(sessionId);
            return await dbSet.Where(s => s.ID == sessionId)
            .Include(s => s.Galaxy).ThenInclude(g => g.GameMap)
            .Include(s => s.Players).ThenInclude(p => p.User)
            .Include(s => s.Players).ThenInclude(p => p.PlayerColor)
            .Include(s => s.Players).ThenInclude(p => p.Planets)
            .Include(a => a.Galaxy).ThenInclude(g => g.Planets)
            .FirstOrDefaultAsync();
        }

        public override async Task<bool> Delete(Guid id)
        {
            var session = await dbSet.Where(x => x.ID == id)
                .Include(session => session.Players)
                .Include(session => session.Galaxy)
                    .ThenInclude(galaxy => galaxy.Planets)
                .FirstOrDefaultAsync();
            if (session != null)
            {
                dbSet.Remove(session);
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



        public async Task<Session> GetByNameAndCode(string name, string gameCode)
        {
            var exist = await dbSet.Where(x => x.Name == name && x.GameCode == gameCode).FirstOrDefaultAsync();
            return exist;   // if (exist == null) return exist;
        }
    }
}