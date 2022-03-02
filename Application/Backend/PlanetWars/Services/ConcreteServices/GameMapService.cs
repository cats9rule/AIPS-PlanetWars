using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;

namespace PlanetWars.Services.ConcreteServices
{
    public class GameMapService : IGameMapService
    {

        private readonly IUnitOfWork _unitOfWork;

        public GameMapService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<bool> AddGameMap(GameMap gm)
        {
            await _unitOfWork.GameMaps.Add(gm);
            return true;
        }

        public async Task<IEnumerable<GameMap>> GetAll()
        {
            return await _unitOfWork.GameMaps.GetAll();
        }

        public async Task<IEnumerable<GameMap>> GetByPlanetCount(int planetCount)
        {
            return await _unitOfWork.GameMaps.GetByPlanetCount(planetCount);
        }

        public async Task<IEnumerable<GameMap>> GetByResourcePlanetRatio(float ratio)
        {
            return await _unitOfWork.GameMaps.GetByResourcePlanetRatio(ratio);
        }
    }
}