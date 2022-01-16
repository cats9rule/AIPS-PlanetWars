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

        // public async Task<bool> Add(PlayerDto playerDto)
        // {
        //     return await _unitOfWork.Players.Add(DtoToModel(playerDto));
        // }

        public async Task<bool> Delete(Guid id)
        {
            using(_unitOfWork)
            {
                var retval = await _unitOfWork.Players.Delete(id);
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            using(_unitOfWork)
            {
                // IEnumerable<Player> players = await _unitOfWork.Players.GetAll();
                // List<PlayerDto> playerDtos = new List<PlayerDto>();
                // foreach (Player player in players)
                // {
                //     playerDtos.Add(ModelToDto(player));
                // }
                // return playerDtos;
                return await _unitOfWork.Players.GetAll();
            }
        }

        public async Task<Player> GetById(Guid id)
        {
            using (_unitOfWork)
            {
                Player player = await _unitOfWork.Players.GetById(id);
                // if (player != null)
                // {
                //     return player;
                // }
                // return null;
                return player;
            }
        }

        public async Task<IEnumerable<Player>> GetByUserId(Guid id)
        {
            using (_unitOfWork)
            {
                // IEnumerable<Player> players = await _unitOfWork.Players.GetByUserId(id);
                // if (players != null)
                // {
                //     List<PlayerDto> dtoList = new List<PlayerDto>();
                //     foreach(Player player in players)
                //     {
                //         dtoList.Add(ModelToDto(player));
                //     }
                //     return dtoList;
                // }
                // return null;
                return await _unitOfWork.Players.GetByUserId(id);
            }
        }

        public async Task<IEnumerable<Player>> GetByUsernameAndTag(string username, string tag)
        {
            using (_unitOfWork)
            {
                // IEnumerable<Player> players = await _unitOfWork.Players.GetByUsernameAndTag(username, tag);

                // if (players != null)
                // {
                //     List<PlayerDto> dtoList = new List<PlayerDto>();
                //     foreach(Player player in players)
                //     {
                //         dtoList.Add(ModelToDto(player));
                //     }
                //     return dtoList;
                // }
                // return null;
                return await _unitOfWork.Players.GetByUsernameAndTag(username, tag);
            }
        }

        public async Task<bool> Update(UpdatePlayerDto playerDto)
        {
            using (_unitOfWork)
            {
                // var retval = await _unitOfWork.Players.Update(DtoToModel(playerDto));

                var player = await _unitOfWork.Players.GetById(playerDto.PlayerID);

                if(playerDto.ShouldAddPlanet == true)
                {
                    var planet = await _unitOfWork.Planets.GetById(playerDto.AddPlanetID);
                    player.Planets.Add(planet);
                }

                if(playerDto.ShouldRemovePlanet == true)
                {
                    var planet = await _unitOfWork.Planets.GetById(playerDto.RemovePlayerID);
                    player.Planets.Remove(planet);
                }

                if(playerDto.ChangeActivity == true)
                {
                    player.IsActive = !player.IsActive;
                }

                var retval = await _unitOfWork.Players.Update(player);
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<Player> CreatePlayer(Guid userId, int turnIndex, Guid sessionId)
        {
            using(_unitOfWork)
            {
                User user = await _unitOfWork.Users.GetById(userId);
                Session session = await _unitOfWork.Sessions.GetById(sessionId);

                Player player = new Player();
                player.User = user;
                player.UserID = userId;
                player.PlayerColor = await _unitOfWork.PlayerColors.GetByTurnIndex(turnIndex);
                player.IsActive = true;
                player.SessionID = sessionId;
                player.Session = session;
                player.Planets = new List<Planet>();

                await _unitOfWork.Players.Add(player);
                await _unitOfWork.CompleteAsync();

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