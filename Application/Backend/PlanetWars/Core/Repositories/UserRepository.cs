using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanetWars.Core.IRepositories;
using PlanetWars.Data.Context;
using PlanetWars.Data.Models;

namespace PlanetWars.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PlanetWarsDbContext context)
        : base(context)
        {

        }

        public override Task<bool> Add(User entity)
        {
            return base.Add(entity);
        }

        public override Task<IEnumerable<User>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<User> GetById(Guid id)
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

        public override async Task<bool> Update(User entity)
        {
            var existing = await dbSet.Where(x => x.ID == entity.ID).FirstOrDefaultAsync();
            if (existing == null)
            {
                return await Add(entity);
            }
            dbSet.Update(entity);
            return true;
        }

        public async Task<User> GetByUsername(string username)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> GetByTag(string tag)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Tag == tag);
        }

        public async Task<User> GetByDisplayedName(string displayedName)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.DisplayedName == displayedName);
        }

        public async Task<User> GetByUsernameAndTag(string username, string tag)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Username == username && x.Tag == tag);
        }
    }
}