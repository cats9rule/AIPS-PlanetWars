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
                var retval = await _unitOfWork.Planets.Add(DtoToModel(planetDto));
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<IEnumerable<PlanetDto>> CreatePlanets(int planetCount, bool hasResource)
        {
            using(_unitOfWork)
            {
                List<PlanetDto> planetList = new List<PlanetDto>();
                for(int i = 0; i < planetCount; i++)
                {
                    Planet planet = CreatePlanet(hasResource);
                    planetList.Add(ModelToDto(planet));
                    var retval = await _unitOfWork.Planets.Add(planet);
                }
                await _unitOfWork.CompleteAsync();
                return planetList;
            }
        }

        private Planet CreatePlanet(bool hasResource)
        {
            //FIXME: possible that planet doesnt have a resource even though hasResource is True
            Planet planet = new Planet();
            planet.ID = new Guid();
            planet.ArmyCount = 0;
            planet.Owner = null;
            planet.NeighbourPlanets = new List<PlanetPlanet>();
            if (hasResource)
            {
                Random rnd = new Random();
                int num = rnd.Next();
                if (num % 3 == 0)
                {
                    planet.DefenseBoost = defBoost;
                }
                if (num % 4 == 0)
                {
                    planet.AttackBoost = atkBoost;
                }
                if (num % 5 == 0)
                {
                    planet.MovementBoost = movBoost;
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
                IEnumerable<Planet> planets = await _unitOfWork.Planets.GetAll();
                return planets.Select(planet => ModelToDto(planet)).ToList();
            }
        }

        public async Task<PlanetDto> GetById(Guid id)
        {
            using (_unitOfWork)
            {
                var planet = await _unitOfWork.Planets.GetById(id);
                if (planet != null)
                {
                    return ModelToDto(planet);
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
                    List<PlanetDto> planets = player.Planets.Select(p => ModelToDto(p)).ToList();
                    return planets;
                }
                return new List<PlanetDto>();
            }
        }

        public async Task<bool> Update(PlanetDto planetDto)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.Planets.Update(DtoToModel(planetDto));
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
                        relatedPlanets.Add(ModelToDto(planet));
                    }
                }
                return relatedPlanets;
            }
        }

        #region Mappers
        //TODO: implement Automapper
        public static PlanetDto ModelToDto(Planet model)
        {
            return new PlanetDto
            {
                ID = model.ID,
                Owner = model.Owner.ID,
                ArmyCount = model.ArmyCount,
                NeighbourPlanets = model.NeighbourPlanets.Select(planet => planet.PlanetToID).ToList(),
                MovementBoost = model.MovementBoost,
                DefenseBoost = model.DefenseBoost,
                AttackBoost = model.AttackBoost
            };
        }
        public static Planet DtoToModel(PlanetDto dto)
        {

            return new Planet
            {
                ID = dto.ID,
                ArmyCount = dto.ArmyCount,
                MovementBoost = dto.MovementBoost,
                AttackBoost = dto.AttackBoost,
                DefenseBoost = dto.DefenseBoost,
            };
        }

        #endregion
    }
}