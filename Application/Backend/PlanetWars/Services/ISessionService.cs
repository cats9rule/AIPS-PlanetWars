using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.DTOs;
using PlanetWars.Data.Models;

namespace PlanetWars.Services
{
    public interface ISessionService
    {
        public Task<Session> CreateSession( CreateGameDto dto /*string name, string password, int maxPlayers*/);//, Galaxy galaxy, Player player);
        public Task<Session> InitializeSession(Session session, Guid galaxyId, Guid playerId);
        public Task<bool> Add(SessionDto session);
        public Task<IEnumerable<SessionDto>> GetAllSessions();
        public Task<SessionDto> GetById(Guid id);
        public Task<IEnumerable<SessionDto>> GetByName(string name);
        public Task<bool> Update(SessionDto session);
        public Task<bool> Delete(Guid id);
        public Task<Player> AddPlayer(Guid sessionId, Player player);
    } 
}