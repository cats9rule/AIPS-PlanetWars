using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.ConcreteServices
{

    //TODO: fix all methods to return SessionDtos instead of Session

    public class SessionService : ISessionService
    {
        #region Attributes
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        #endregion

        public SessionService(IUnitOfWork uow, IMapper mapper)
        {
            _unitOfWork = uow;
            _mapper = mapper;
        }

        public async Task<bool> Add(Session session)
        {
            return await _unitOfWork.Sessions.Add(session);
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

        public async Task<IEnumerable<SessionDto>> GetByName(string name)
        {
            using (_unitOfWork)
            {
                return _mapper.Map<List<Session>, List<SessionDto>>(new List<Session>(await _unitOfWork.Sessions.GetByName(name)));
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
                var retval = await _unitOfWork.Sessions.Delete(id);
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<SessionDto> CreateSession(CreateGameDto dto)
        {
            //FIXME: clean this up and make it work normally.
            using (_unitOfWork)
            {
                GameMap gameMap = await _unitOfWork.GameMaps.GetById(dto.GameMapID);
                User user = await _unitOfWork.Users.GetById(dto.UserId);
                if (gameMap == null || user == null) return null;

                Session session = await InitSession(dto.Name, dto.Password, gameMap.MaxPlayers);
                if (session == null) return null;

                Player player = new Player()
                {
                    User = user,
                    UserID = user.ID,
                    PlayerColor = await _unitOfWork.PlayerColors.GetByTurnIndex(0),
                    IsActive = true,
                    Session = session,
                    SessionID = session.ID,
                    TotalArmies = 5,
                    Planets = new List<Planet>()
                };
                var result = await _unitOfWork.Players.Add(player);
                if (result)
                {
                    session.CreatorID = player.ID;
                    session.Players.Add(player);
                    session.PlayerCount++;
                }
                else return null;

                Galaxy galaxy = new Galaxy()
                {
                    ResourcePlanetRatio = gameMap.ResourcePlanetRatio,
                    PlanetCount = gameMap.PlanetCount,
                    Session = session,
                    SessionID = session.ID,
                    Planets = new List<Planet>()
                };
                result = await _unitOfWork.Galaxies.Add(galaxy);
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

        private async Task<Session> InitSession(string name, string password, int maxPlayers)
        {
            Session session = new Session()
            {
                Name = name,
                MaxPlayers = maxPlayers,
                Password = password,
                IsPrivate = password == "" ? false : true,
                Players = new List<Player>(),
                PlayerCount = 0,
                CurrentTurnIndex = 0
            };
            var result = await _unitOfWork.Sessions.Add(session);
            await _unitOfWork.CompleteAsync();
            return result ? session : null;
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
    }
}