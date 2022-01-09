using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.ConcreteServices
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlayerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Add(PlayerDto playerDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlayerDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PlayerDto> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerDto> GetByUserId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerDto> GetByUsernameAndTag(string username, string tag)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PlayerDto playerDto)
        {
            throw new NotImplementedException();
        }

        #region Mappers
        public static PlayerDto ModelToDto(Player model)
        {
            List<Guid> planetIDs = model.Planets.Select(planet => planet.ID).ToList();
            return new PlayerDto
            {
                ID = model.ID,
                UserID = model.User.ID,
                Color = model.Color.Color.HexValue,
                TurnIndex = model.Color.TurnIndex,
                PlanetIDs = planetIDs,
                IsActive = model.IsActive
            };
        }
        // public static Player DtoToModel(PlayerDto dto)
        // {
            
        //     return new Player
        //     {
        //         ID = dto.ID,
        //         Username = dto.Username,
        //         Tag = dto.Tag,
        //         DisplayedName = dto.DisplayedName
        //     };
        // }
        #endregion
    }
}