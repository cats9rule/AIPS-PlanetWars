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

        public async Task<bool> Add(PlayerDto playerDto)
        {
            return await _unitOfWork.Players.Add(DtoToModel(playerDto));
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _unitOfWork.Players.Delete(id);
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

        public async Task<IEnumerable<PlayerDto>> GetByUserId(Guid id)
        {
            using (_unitOfWork)
            {
                IEnumerable<Player> players = await _unitOfWork.Players.GetByUserId(id);
                if (players != null)
                {
                    List<PlayerDto> dtoList = new List<PlayerDto>();
                    foreach(Player player in players)
                    {
                        dtoList.Add(ModelToDto(player));
                    }
                    return dtoList;
                }
                return null;
            }
        }

        public async Task<IEnumerable<PlayerDto>> GetByUsernameAndTag(string username, string tag)
        {
            using (_unitOfWork)
            {
                IEnumerable<Player> players = await _unitOfWork.Players.GetByUsernameAndTag(username, tag);

                if (players != null)
                {
                    List<PlayerDto> dtoList = new List<PlayerDto>();
                    foreach(Player player in players)
                    {
                        dtoList.Add(ModelToDto(player));
                    }
                    return dtoList;
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

        public async Task<Player> CreatePlayer(Guid userId)
        {
            using(_unitOfWork)
            {
                User user = await _unitOfWork.Users.GetById(userId);

                Player player = new Player();
                player.User = user;

                return player;
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