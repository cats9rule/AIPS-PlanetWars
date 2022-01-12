using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.ConcreteServices
{
    public class PlanetService : IPlanetService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlanetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public Task<PlanetDto> CreatePlanet()
        {
            throw new NotImplementedException();
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

        public Task<IEnumerable<PlanetDto>> GetForPlayer(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(PlanetDto planetDto)
        {
            var retval = await _unitOfWork.Planets.Update(DtoToModel(planetDto));
            await _unitOfWork.CompleteAsync();
            return retval;
        }

        public Task<bool> UpdateArmies(Guid id, int armyDifference)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOwnership(Guid id, Guid ownerID)
        {
            throw new NotImplementedException();
        }

        #region Mappers
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