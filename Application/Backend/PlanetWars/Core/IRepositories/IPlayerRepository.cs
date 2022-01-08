using System;
using System.Threading.Tasks;
using PlanetWars.Data.Models;

namespace PlanetWars.Core.IRepositories
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
        public Task<Player> GetByUsernameAndTag(string username, string tag);
        public Task<Player> GetByUserID(Guid id);
    }
}