using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PlanetWars.Communication;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.ConcreteServices
{
    public class SessionService : ISessionService
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HubService _hubService;
        #endregion

        public SessionService(IUnitOfWork uow, IMapper mapper)
        {
            _unitOfWork = uow;
            _mapper = mapper;
        }

        public async Task<bool> Add(Session session)
        {
            using (_unitOfWork)
            {
                return await _unitOfWork.Sessions.Add(session);
            }
        }

        public async Task<IEnumerable<SessionDto>> GetAllSessions()
        {
            using (_unitOfWork)
            {
                return _mapper.Map<List<Session>, List<SessionDto>>(new List<Session>(await _unitOfWork.Sessions.GetAll()));
            }
        }

        public async Task<SessionDto> GetById(Guid sessionId)
        {
            using (_unitOfWork)
            {
                return _mapper.Map<Session, SessionDto>(await _unitOfWork.Sessions.GetById(sessionId));
            }
        }

        public async Task<SessionDto> GetByNameAndCode(string name, string code)
        {
            using (_unitOfWork)
            {
                return _mapper.Map<Session, SessionDto>(await _unitOfWork.Sessions.GetByNameAndCode(name, code));
            }
        }

        public async Task<bool> Update(UpdateSessionDto sessionDto)
        {
            using (_unitOfWork)
            {
                var session = await _unitOfWork.Sessions.GetById(sessionDto.SessionID);

                if (sessionDto.HasLostPlanet == true && sessionDto.HasWonPlanet)
                {
                    var loser = await _unitOfWork.Players.GetById(sessionDto.PlanetLoserID);
                    var planet = await _unitOfWork.Planets.GetById(sessionDto.PlanetID);
                    loser.Planets.Remove(planet);

                    await _unitOfWork.Players.Update(loser);
                }

                if (sessionDto.HasWonPlanet == true)
                {
                    var winner = await _unitOfWork.Players.GetById(sessionDto.PlanetWinnerID);
                    var planet = await _unitOfWork.Planets.GetById(sessionDto.PlanetID);
                    winner.Planets.Add(planet);
                    planet.OwnerID = winner.ID;
                    planet.Owner = winner;

                    await _unitOfWork.Planets.Update(planet);
                    await _unitOfWork.Players.Update(winner);
                }

                if (sessionDto.NextPlayer == true)
                {
                    session.CurrentTurnIndex = (session.CurrentTurnIndex++) % session.Players.Count;
                }

                var retval = await _unitOfWork.Sessions.Update(session);
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (_unitOfWork)
            {
                List<Planet> sessionPlanets = new List<Planet>(await _unitOfWork.Planets.GetForSession(id));
                List<PlanetPlanet> relations = new List<PlanetPlanet>();
                foreach (Planet planet in sessionPlanets)
                {
                    relations.AddRange(await _unitOfWork.PlanetPlanets.DeleteAllRelationsForPlanet(planet.ID));
                }
                await _unitOfWork.CompleteAsync();
                var retval = await _unitOfWork.Sessions.Delete(id);
                if (retval)
                {
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                    }
                    catch (Exception e)
                    {
                        await _unitOfWork.PlanetPlanets.AddAll(relations);
                        Console.Error.Write(e.Message);
                        Console.Error.Write(e.StackTrace);
                    }

                }
                return retval;
            }
        }

        public async Task<SessionDto> CreateSession(CreateGameDto dto)
        {
            using (_unitOfWork)
            {
                GameMap gameMap = await _unitOfWork.GameMaps.GetById(dto.GameMapID);
                User user = await _unitOfWork.Users.GetById(dto.CreatorID);
                if (gameMap == null || user == null) return null;

                Session session = await InitSession(dto.Name, gameMap.MaxPlayers);
                if (session == null) return null;

                Player player = new Player()
                {
                    User = user,
                    UserID = user.ID,
                    PlayerColor = await _unitOfWork.PlayerColors.GetByTurnIndex(0),
                    IsActive = true,
                    Session = session,
                    IsSessionOwner = true,
                    SessionID = session.ID,
                    TotalArmies = 5, //TODO: define this parameter
                    Planets = new List<Planet>()
                };

                session.Players.Add(player);
                session.PlayerCount++;

                Galaxy galaxy = new Galaxy()
                {
                    ResourcePlanetRatio = gameMap.ResourcePlanetRatio,
                    PlanetCount = gameMap.PlanetCount,
                    Session = session,
                    SessionID = session.ID,
                    Planets = new List<Planet>(),
                    GameMapID = dto.GameMapID
                };
                var result = await _unitOfWork.Galaxies.Add(galaxy);
                if (result)
                {
                    session.Galaxy = galaxy;
                }
                else return null;

                await _unitOfWork.Sessions.Update(session);
                await _unitOfWork.CompleteAsync();


                return _mapper.Map<Session, SessionDto>(session);
            }
        }

        public async Task<PlayerDto> AddPlayer(Guid sessionID, PlayerDto player)
        {
            using (_unitOfWork)
            {
                Session session = await _unitOfWork.Sessions.GetById(sessionID);
                if (session.PlayerCount == session.MaxPlayers)
                {
                    return null;
                }
                player.TurnIndex = session.Players.Count;
                session.Players.Add(_mapper.Map<PlayerDto, Player>(player));
                session.PlayerCount++;
                await _unitOfWork.Sessions.Update(session);
                await _unitOfWork.CompleteAsync();

                return player;
            }
        }
        private async Task<Session> InitSession(string name, int maxPlayers)
        {

            Session session = new Session()
            {
                Name = name,
                MaxPlayers = maxPlayers,
                GameCode = GetRandomString(6),
                Players = new List<Player>(),
                PlayerCount = 0,
                CurrentTurnIndex = 0
            };
            var result = await _unitOfWork.Sessions.Add(session);
            await _unitOfWork.CompleteAsync();
            return result ? session : null;
        }

        //TODO: test this
        public async Task<bool> LeaveGame(LeaveGameDto dto)
        {
            using (_unitOfWork)
            {
                Session session = await _unitOfWork.Sessions.GetById(dto.SessionID);
                Player player = session.Players.Where(player => player.ID == dto.PlayerID).FirstOrDefault();
                if (player != null)
                {
                    User user = await _unitOfWork.Users.GetById(player.UserID);
                    var success = await _unitOfWork.Players.Delete(dto.PlayerID);
                    session.PlayerCount--;
                    if (success)
                    {
                        await _unitOfWork.CompleteAsync();
                        var notification = new LeaveGameNotificationDto()
                        {
                            Message = $"{user.Username}#{user.Tag} has left the game.",
                            PlayerID = player.ID,
                            SessionID = dto.SessionID
                        };
                        await _hubService.NotifyPlayerLeft(notification);
                        return true;

                    }
                }
                return false;
            }
        }

        private string GetRandomString(int size) {
            char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[4*size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;
                result.Append(chars[idx]);
            }
            return result.ToString();
        }

    }
}