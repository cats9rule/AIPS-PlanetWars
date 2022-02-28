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
            using(_unitOfWork)
            {
                // IEnumerable<Session> sessions = await _unitOfWork.Sessions.GetAll();
                // List<SessionDto> sessionDtos = new List<SessionDto>();
                // foreach(Session session in sessions)
                // {
                //     sessionDtos.Add(ModelToDto(session));
                // }
                // return sessionDtos;
                return _mapper.Map<List<Session>, List<SessionDto>>(new List<Session>(await _unitOfWork.Sessions.GetAll()));
            }
        }

        public async Task<SessionDto> GetById(Guid sessionId)
        {
            using(_unitOfWork)
            {
                // Session session = await _unitOfWork.Sessions.GetById(id);
                // if(session != null)
                //     return session;
                // return null;
                return _mapper.Map<Session, SessionDto>(await _unitOfWork.Sessions.GetById(sessionId));
            }
        }

        public async Task<IEnumerable<SessionDto>> GetByName(string name)
        {
            using(_unitOfWork)
            {
                // IEnumerable<Session> sessions = await _unitOfWork.Sessions.GetByName(name);
                // List<SessionDto> dtos = new List<SessionDto>();
                // foreach(Session s in sessions)
                // {
                //     dtos.Add(ModelToDto(s));
                // }
                // return dtos;
                return _mapper.Map<List<Session>, List<SessionDto>>(new List<Session>(await _unitOfWork.Sessions.GetByName(name)));
            }
        }

        public async Task<bool> Update(UpdateSessionDto sessionDto)
        {
            using(_unitOfWork)
            {
                var session = await _unitOfWork.Sessions.GetById(sessionDto.SessionID);

                if(sessionDto.HasLostPlanet == true && sessionDto.HasWonPlanet)
                {
                    var loser = await _unitOfWork.Players.GetById(sessionDto.PlanetLoserID);
                    var planet = await _unitOfWork.Planets.GetById(sessionDto.PlanetID);
                    loser.Planets.Remove(planet);

                    await _unitOfWork.Players.Update(loser);
                }

                if(sessionDto.HasWonPlanet == true)
                {
                    var winner = await _unitOfWork.Players.GetById(sessionDto.PlanetWinnerID);
                    var planet = await _unitOfWork.Planets.GetById(sessionDto.PlanetID);
                    winner.Planets.Add(planet);
                    planet.OwnerID = winner.ID;
                    planet.Owner = winner;

                    await _unitOfWork.Planets.Update(planet);
                    await _unitOfWork.Players.Update(winner);
                }

                if(sessionDto.NextPlayer == true)
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
            using(_unitOfWork)
            {
                var retval = await _unitOfWork.Sessions.Delete(id);
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<SessionDto> CreateSession( CreateGameDto dto /*string name, string password, int maxPlayers, Galaxy galaxy, Player player*/)
        {
            //FIXME: clean this up and make it work normally.
            using(_unitOfWork)
            {
                var session = new Session();
                session.Name = dto.Name;
                session.MaxPlayers = dto.MaxPlayers;
                session.Password = dto.Password;
                if(session.Password == "") 
                    session.IsPrivate = false;
                else session.IsPrivate = true;
                session.Players = new List<Player>();

                await _unitOfWork.Sessions.Add(session);
                await _unitOfWork.CompleteAsync();

                //AKO SALJETE GALAXY I PLAYER KAO PARAMETRE ONDA VAM NE TREBA OVO SA NEW
                //AKO HOCETE SA NJU ONDA MORATE DA POSALJETE STA JE POTREBNO OD PARAMETARA KROZ FUNKCIJU

                User user = await _unitOfWork.Users.GetById(dto.UserId);
                Player player = new Player();
                player.User = user;
                player.UserID = dto.UserId;
                player.PlayerColor = await _unitOfWork.PlayerColors.GetByTurnIndex(0);
                player.IsActive = true;
                player.SessionID = session.ID;
                player.Session = session;

                await _unitOfWork.Players.Add(player);

                Galaxy galaxy = new Galaxy();
                galaxy.ResourcePlanetRatio = dto.ResourcePlanetRatio;
                galaxy.PlanetCount = dto.PlanetCount;
                galaxy.SessionID = session.ID;
                galaxy.Session = session;

                await _unitOfWork.Galaxies.Add(galaxy);

                session.Players.Add(player);
                session.PlayerCount = 1;
                session.CreatorID = player.ID;
                session.Galaxy = galaxy;

                await _unitOfWork.Sessions.Update(session);
                await _unitOfWork.CompleteAsync();

                return _mapper.Map<Session, SessionDto>(session);
            }
        }

        public async Task<SessionDto> InitializeSession(Session session, Guid galaxyId, Guid playerId)
        {
            //FIXME: should receive sessionID, not whole session
            using(_unitOfWork)
            {
                session.Galaxy = await _unitOfWork.Galaxies.GetById(galaxyId);
                session.CreatorID = playerId;

                await _unitOfWork.Sessions.Update(session);
                await _unitOfWork.CompleteAsync();
                return _mapper.Map<Session, SessionDto>(session);
            }
        }

        public async Task<PlayerDto> AddPlayer(SessionDto session, PlayerDto player)
        {
            //FIXME: should receive sessionID, not whole session
            using(_unitOfWork)
            {
                //Session session = await _unitOfWork.Sessions.GetById(sessionId);

                if(session.PlayerCount == session.MaxPlayers)
                {
                    return null;
                }

                player.TurnIndex = session.Players.Count;

                session.Players.Add(player.ID);
                session.PlayerCount++;
                await _unitOfWork.Sessions.Update(_mapper.Map<SessionDto, Session>(session));
                await _unitOfWork.CompleteAsync();
                return player;
            }
        }
    }
}