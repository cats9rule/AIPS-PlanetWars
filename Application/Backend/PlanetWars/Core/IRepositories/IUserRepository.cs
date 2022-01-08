using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.Data.Models;

namespace PlanetWars.Core.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
         Task<IEnumerable<User>> GetByUsername(string username);
         Task<IEnumerable<User>> GetByTag(string tag);
         Task<IEnumerable<User>> GetByDisplayedName(string displayedName);
         Task<User> GetByUsernameAndTag(string username, string tag);
    }
}