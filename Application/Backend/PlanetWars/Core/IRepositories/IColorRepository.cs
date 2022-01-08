using System.Threading.Tasks;
using PlanetWars.Data.Models;

namespace PlanetWars.Core.IRepositories
{
    public interface IColorRepository : IGenericRepository<Color>
    {
         public Task<Color> GetByHexValue(string hexValue);
    }
}