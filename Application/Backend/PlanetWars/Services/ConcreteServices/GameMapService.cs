using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.ConcreteServices
{
    public class GameMapService : IGameMapService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameMapService(IUnitOfWork uow, IMapper mapper)
        {
            _unitOfWork = uow;
            _mapper = mapper;
        }

        public async Task<bool> AddGameMap(GameMap gm)
        {
            using (_unitOfWork)
            {
                var result = await _unitOfWork.GameMaps.Add(gm);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }

        public async Task<IEnumerable<GameMapDto>> GetAll()
        {
            using (_unitOfWork)
            {
                return _mapper.Map<List<GameMapDto>>(await _unitOfWork.GameMaps.GetAll());
            }
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