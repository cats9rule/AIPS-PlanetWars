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
        public Task<bool> Add(Session session);
        public Task<IEnumerable<Session>> GetAllSessions();
        public Task<Session> GetById(Guid id);
        public Task<IEnumerable<Session>> GetByName(string name);
        public Task<bool> Update(UpdateSessionDto sessionDto);
        public Task<bool> Delete(Guid id);
        public Task<Player> AddPlayer(Guid sessionId, Player player);
    } 
}