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
            return _unitOfWork.Players.Add(DtoToModel(playerDto));
        }

        public Task<bool> Delete(Guid id)
        {
            return _unitOfWork.Players.Delete(id);
        }

        public async Task<IEnumerable<PlayerDto>> GetAll()
        {
            IEnumerable<Player> players = await _unitOfWork.Players.GetAll();
            List<PlayerDto> playerDtos = new List<PlayerDto>();
            foreach (Player player in players)
            {
                playerDtos.Add(ModelToDto(player));
            }
            return playerDtos;
        }

        public async Task<PlayerDto> GetById(Guid id)
        {
            using (_unitOfWork)
            {
                Player player = await _unitOfWork.Players.GetById(id);
                if (player != null)
                {
                    return ModelToDto(player);
                }
                return null;
            }
        }

        public async Task<PlayerDto> GetByUserId(Guid id)
        {
            using (_unitOfWork)
            {
                Player player = await _unitOfWork.Players.GetByUserId(id);
                if (player != null)
                {
                    return ModelToDto(player);
                }
                return null;
            }
        }

        public async Task<PlayerDto> GetByUsernameAndTag(string username, string tag)
        {
            using (_unitOfWork)
            {
                Player player = await _unitOfWork.Players.GetByUsernameAndTag(username, tag);
                if (player != null)
                {
                    return ModelToDto(player);
                }
                return null;
            }
        }

        public async Task<bool> Update(PlayerDto playerDto)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.Players.Update(DtoToModel(playerDto));
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        #region Mappers
        public static PlayerDto ModelToDto(Player model)
        {
            List<Guid> planetIDs = model.Planets.Select(planet => planet.ID).ToList();
            return new PlayerDto
            {
                ID = model.ID,
                UserID = model.User.ID,
                PlayerColor = model.PlayerColor.ColorHexValue,
                TurnIndex = model.PlayerColor.TurnIndex,
                PlanetIDs = planetIDs,
                IsActive = model.IsActive
            };
        }
        public static Player DtoToModel(PlayerDto dto)
        {
            //TODO: FIX THIS ASAP!!!
            return new Player
            {
                ID = dto.ID,
                IsActive = dto.IsActive
            };
        }
        #endregion
    }
}