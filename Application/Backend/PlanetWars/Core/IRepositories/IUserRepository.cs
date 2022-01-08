using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.Data.Models;

namespace PlanetWars.Core.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
         Task<User> GetByUsername(string username);
         Task<User> GetByTag(string tag);
         Task<User> GetByDisplayedName(string displayedName);
         Task<User> GetByUsernameAndTag(string username, string tag);
    }
}