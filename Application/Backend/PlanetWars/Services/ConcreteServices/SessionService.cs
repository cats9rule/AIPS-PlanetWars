using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.ConcreteServices
{
    public class SessionService : ISessionService
    {
        #region Attributes
        public readonly IUnitOfWork _unitOfWork;
        #endregion

        public SessionService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<bool> Add(SessionDto dto)
        {
            return await _unitOfWork.Sessions.Add(DtoToModel(dto));
        }

        public async Task<IEnumerable<SessionDto>> GetAllSessions()
        {
            using(_unitOfWork)
            {
                IEnumerable<Session> sessions = await _unitOfWork.Sessions.GetAll();
                List<SessionDto> sessionDtos = new List<SessionDto>();
                foreach(Session session in sessions)
                {
                    sessionDtos.Add(ModelToDto(session));
                }
                return sessionDtos;
            }
        }

        public async Task<SessionDto> GetById(Guid id)
        {
            using(_unitOfWork)
            {
                Session session = await _unitOfWork.Sessions.GetById(id);
                if(session != null)
                    return ModelToDto(session);
                return null;
            }
        }

        public async Task<IEnumerable<SessionDto>> GetByName(string name)
        {
            using(_unitOfWork)
            {
                IEnumerable<Session> sessions = await _unitOfWork.Sessions.GetByName(name);
                List<SessionDto> dtos = new List<SessionDto>();
                foreach(Session s in sessions)
                {
                    dtos.Add(ModelToDto(s));
                }
                return dtos;
            }
        }

        public async Task<bool> Update(SessionDto dto)
        {
            using(_unitOfWork)
            {
                var retval = await _unitOfWork.Sessions.Update(DtoToModel(dto));
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using(_unitOfWork)
            {
                return await _unitOfWork.Sessions.Delete(id);
            }
        }

        public async Task<Session> CreateSession( CreateGameDto dto /*string name, string password, int maxPlayers, Galaxy galaxy, Player player*/)
        {
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
                session.CreatorID = player.ID;
                session.Galaxy = galaxy;

                await _unitOfWork.Sessions.Update(session);
                await _unitOfWork.CompleteAsync();

                return session;
            }
        }

        public async Task<Session> InitializeSession(Session session, Guid galaxyId, Guid playerId)
        {
            using(_unitOfWork)
            {
                session.Galaxy = await _unitOfWork.Galaxies.GetById(galaxyId);
                session.CreatorID = playerId;

                await _unitOfWork.Sessions.Update(session);
                await _unitOfWork.CompleteAsync();
                return session;
            }
        }

        public async Task<Player> AddPlayer(Guid sessionId, Player player)
        {
            using(_unitOfWork)
            {
                Session session = await _unitOfWork.Sessions.GetById(sessionId);

                if(session.Players.Count == session.MaxPlayers)
                {
                    return null;
                }

                player.PlayerColor.TurnIndex = session.Players.Count;
                await _unitOfWork.Players.Add(player);

                session.Players.Add(player);
                await _unitOfWork.Sessions.Update(session);
                await _unitOfWork.CompleteAsync();
                return player;
            }
        }

        #region Mappers
        public static Session DtoToModel(SessionDto dto)
        {
            Session model = new Session();

            return model;
        }

        public static SessionDto ModelToDto(Session model)
        {
            SessionDto dto = new SessionDto();

            return dto;
        }
        #endregion
    }
}