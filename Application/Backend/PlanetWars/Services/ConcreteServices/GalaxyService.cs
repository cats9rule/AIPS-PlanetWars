using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PlanetWars.Core.Configuration;
using PlanetWars.DTOs;
using PlanetWars.Data.Models;


namespace PlanetWars.Services.ConcreteServices
{
    public class GalaxyService : IGalaxyService
    {
        #region Atributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion


        public GalaxyService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<Galaxy> CreateGalaxy(int planetCount, float resourcePlanetRatio, Guid sessionId)
        {
            using(_unitOfWork)
            {
                Galaxy galaxy = new Galaxy();
                //GalaxyDto dto = new GalaxyDto();

                galaxy.ResourcePlanetRatio = resourcePlanetRatio;
                galaxy.PlanetCount = planetCount;
                // dto.ResourcePlanetRatio = resourcePlanetRatio;
                // dto.PlanetCount = planetCount;

                galaxy.SessionID = sessionId;
                galaxy.Session = await _unitOfWork.Sessions.GetById(sessionId);

                //proveriti da li nesto fali --- mappere ne koristiti!

                await _unitOfWork.Galaxies.Add(galaxy);
                await _unitOfWork.CompleteAsync();
                return galaxy;
            }
        }

        public async Task<IEnumerable<GalaxyDto>> GetAllGalaxies()
        {
            using (_unitOfWork)
            {
                IEnumerable<Galaxy> galaxies = await _unitOfWork.Galaxies.GetAll();
                List<GalaxyDto> galaxyDtos = new List<GalaxyDto>();
                foreach(Galaxy galaxy in galaxies)
                {
                    galaxyDtos.Add(ModelToDto(galaxy));
                }
                return galaxyDtos;
            }
        }

        public async Task<GalaxyDto> GetGalaxy(Guid id)
        {
            using (_unitOfWork)
            {
                Galaxy galaxy = await _unitOfWork.Galaxies.GetById(id);
                if (galaxy != null)
                {
                    return ModelToDto(galaxy);
                }
                return null;
            }
        }

        public async Task<IEnumerable<GalaxyDto>> GetGalaxiesByPlanetCount(int count)
        {
            using(_unitOfWork)
            {
                IEnumerable<Galaxy> galaxies = await _unitOfWork.Galaxies.GetGalaxiesByPlanetCount(count);
                List<GalaxyDto> galaxyDtos = new List<GalaxyDto>();
                foreach(Galaxy galaxy in galaxies)
                {
                    galaxyDtos.Add(ModelToDto(galaxy));
                }
                return galaxyDtos;
            }
        }

        public async Task<bool> UpdateGalaxy(GalaxyDto dto)
        {
            using(_unitOfWork)
            {
                var retVal = await _unitOfWork.Galaxies.Update(DtoToModel(dto));
                await _unitOfWork.CompleteAsync();
                return retVal;
            }
        }

        public async Task<bool> DeleteGalaxy(Guid id)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.Galaxies.Delete(id);
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        #region Mappers
        public static Galaxy DtoToModel(GalaxyDto dto)
        {
            Galaxy model = new Galaxy();
            model.ID = dto.ID;
            foreach(var v in dto.Planets)
            {
                Planet p = new Planet{ ID = v };
                model.Planets.Add(p);
            }

            return model;
        }

        public static GalaxyDto ModelToDto(Galaxy model)
        {
            GalaxyDto dto = new GalaxyDto();
            dto.ID = model.ID;
            foreach(var v in model.Planets)
            {
                Guid id = v.ID;
                dto.Planets.Add(id);
            }

            return dto;
        }
        #endregion
    }
}