using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PlanetWars.Data.Models;


namespace PlanetWars.Core.IRepositories
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
        public Task<IEnumerable<Player>> GetByUsernameAndTag(string username, string tag);
        public Task<IEnumerable<Player>> GetByUserId(Guid id);
        // public Task<Player> GetByUsernameAndTag(string username, string tag);
        // public Task<Player> GetByUserId(Guid id);
    }
}