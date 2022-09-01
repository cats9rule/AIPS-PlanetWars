using PlanetWars.Data.Models;
using System.Threading.Tasks;

namespace PlanetWars.Core.IRepositories
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        public Task<Session> GetByNameAndCode(string name, string gameCode);
    }
}