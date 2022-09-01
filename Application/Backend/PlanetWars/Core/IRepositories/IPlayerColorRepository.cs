using System.Threading.Tasks;
using PlanetWars.Data.Models;

namespace PlanetWars.Core.IRepositories
{
    public interface IPlayerColorRepository : IGenericRepository<PlayerColor>
    {
         public Task<PlayerColor> GetByTurnIndex(int turnIndex);
    }
}