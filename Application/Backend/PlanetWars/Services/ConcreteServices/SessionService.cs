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

        public async Task<Session> CreateSession(string name, string password, int maxPlayers, Galaxy galaxy, Player player)
        {
            Session session = new Session();
            session.Name = name;
            session.Galaxy = galaxy;
            session.MaxPlayers = maxPlayers;
            session.Password = password;
            if(session.Password == "") 
                session.IsPrivate = false;
            else session.IsPrivate = true;
            session.Players = new List<Player>();

            await _unitOfWork.Sessions.Add(session);
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.Players.Add(player);
            //await _unitOfWork.CompleteAsync();
            
            session.Players.Add(player);
            session.CreatorID = player.ID;

            var retval = await _unitOfWork.Sessions.Update(session);
            await _unitOfWork.CompleteAsync();

            return session;
        }

        public async Task<Player> AddPlayer(Guid sessionId, Player player)
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