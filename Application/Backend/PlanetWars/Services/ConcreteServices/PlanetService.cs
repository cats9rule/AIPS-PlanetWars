using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.ConcreteServices
{
    public class PlanetService : IPlanetService
    {
        private int atkBoost = 2;
        private int defBoost = 2;
        private int movBoost = 2;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PlanetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Add(PlanetDto planetDto)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.Planets.Add(_mapper.Map<PlanetDto, Planet>(planetDto));
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        //FIXME: all created planets have a resource if "has resource" is true
        public async Task<IEnumerable<PlanetDto>> CreatePlanets(CreateGameDto createGameDto, Guid GalaxyID)
        {
            GameMap gameMap = await _unitOfWork.GameMaps.GetById(createGameDto.GameMapID);
            List<PlanetDto> planetList = new List<PlanetDto>();

            int planetsWithResource = (int)(gameMap.PlanetCount * gameMap.ResourcePlanetRatio);

            for (int i = 0; i < gameMap.PlanetCount; i++)
            {
                Planet planet = i < planetsWithResource ? CreatePlanet(true) : CreatePlanet(false);
                planetList.Add(_mapper.Map<Planet, PlanetDto>(planet));
                var retval = await _unitOfWork.Planets.Add(planet);
            }
            await _unitOfWork.CompleteAsync();
            return planetList;
        }

        private Planet CreatePlanet(bool hasResource)
        {
            Planet planet = new Planet()
            {
                ArmyCount = 0,
                Owner = null,
                NeighbourPlanets = new List<PlanetPlanet>()
            };
            bool madeResource = !hasResource;
            while (!madeResource)
            {
                Random rnd = new Random();
                int num = rnd.Next();
                if (num % 3 == 0)
                {
                    planet.DefenseBoost = defBoost;
                    madeResource = true;
                }
                if (num % 4 == 0)
                {
                    planet.AttackBoost = atkBoost;
                    madeResource = true;
                }
                if (num % 5 == 0)
                {
                    planet.MovementBoost = movBoost;
                    madeResource = true;
                }
            }
            return planet;
        }

        public async Task<bool> Delete(Guid id)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.Planets.Delete(id);
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<IEnumerable<PlanetDto>> GetAll()
        {
            using (_unitOfWork)
            {
                List<Planet> planets = new List<Planet>(await _unitOfWork.Planets.GetAll());
                return _mapper.Map<List<Planet>, List<PlanetDto>>(planets);
            }
        }

        public async Task<PlanetDto> GetById(Guid id)
        {
            using (_unitOfWork)
            {
                var planet = await _unitOfWork.Planets.GetById(id);
                if (planet != null)
                {
                    return _mapper.Map<Planet, PlanetDto>(planet);
                }
                return null;
            }
        }

        public async Task<IEnumerable<PlanetDto>> GetForPlayer(Guid playerId)
        {
            using (_unitOfWork)
            {
                var player = await _unitOfWork.Players.GetById(playerId);
                if (player != null)
                {
                    return _mapper.Map<List<Planet>, List<PlanetDto>>(player.Planets);
                }
                return new List<PlanetDto>();
            }
        }

        public async Task<bool> Update(PlanetDto planetDto)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.Planets.Update(_mapper.Map<PlanetDto, Planet>(planetDto));
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        //FIXME: do we need multiple updaters?
        public Task<bool> UpdateArmies(Guid id, int armyDifference)
        {
            using (_unitOfWork)
            {
                return _unitOfWork.Planets.UpdateArmies(id, armyDifference);
            }
        }

        public Task<bool> UpdateOwnership(Guid id, Guid ownerID)
        {
            using (_unitOfWork)
            {
                return _unitOfWork.Planets.UpdateOwnership(id, ownerID);
            }
        }
        public async Task<IEnumerable<PlanetDto>> GetRelatedPlanets(Guid planetID)
        {
            using (_unitOfWork)
            {
                var relations = await _unitOfWork.PlanetPlanets.GetAllRelationsForPlanet(planetID);
                List<PlanetDto> relatedPlanets = new List<PlanetDto>();
                foreach (PlanetPlanet relation in relations)
                {
                    var planet = await _unitOfWork.Planets.GetById(relation.PlanetToID);
                    if (planet != null)
                    {
                        relatedPlanets.Add(_mapper.Map<Planet, PlanetDto>(planet));
                    }
                }
                return relatedPlanets;
            }
        }
    }
}