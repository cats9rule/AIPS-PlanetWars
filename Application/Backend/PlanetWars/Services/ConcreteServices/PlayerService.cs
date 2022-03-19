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
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PlayerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            using(_unitOfWork)
            {
                var retval = await _unitOfWork.Players.Delete(id);
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<IEnumerable<PlayerDto>> GetAll()
        {
            using(_unitOfWork)
            {
                return _mapper.Map<List<Player>, List<PlayerDto>>(
                    new List<Player>(await _unitOfWork.Players.GetAll())
                );
            }
        }

        public async Task<PlayerDto> GetById(Guid id)
        {
            using (_unitOfWork)
            {
                Player player = await _unitOfWork.Players.GetById(id);
                if (player != null)
                {
                    return _mapper.Map<Player, PlayerDto>(player);
                }
                return null;
            }
        }

        public async Task<IEnumerable<PlayerDto>> GetByUserId(Guid id)
        {
            using (_unitOfWork)
            {
                return _mapper.Map<List<Player>, List<PlayerDto>>(
                    new List<Player>(await _unitOfWork.Players.GetByUserId(id))
                );
            }
        }

        public async Task<IEnumerable<PlayerDto>> GetByUsernameAndTag(string username, string tag)
        {
            using (_unitOfWork)
            {
                return _mapper.Map<List<Player>, List<PlayerDto>>(
                    new List<Player>(
                        await _unitOfWork.Players.GetByUsernameAndTag(username, tag)
                ));
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

        public async Task<PlayerDto> CreatePlayer(Guid userId, int turnIndex, Guid sessionId)
        {
            //TODO: check this method if its needed
            using(_unitOfWork)
            {
                User user = await _unitOfWork.Users.GetById(userId);
                Session session = await _unitOfWork.Sessions.GetById(sessionId);

                Player player = new Player();
                player.User = user;
                player.UserID = userId;
                player.PlayerColor = await _unitOfWork.PlayerColors.GetByTurnIndex(turnIndex);
                player.IsActive = true;
                // player.SessionID = sessionId;
                // player.Session = session;
                player.Planets = new List<Planet>();

                await _unitOfWork.Players.Add(player);
                await _unitOfWork.CompleteAsync();

                return _mapper.Map<Player, PlayerDto>(player);
            }
        }
    }
}