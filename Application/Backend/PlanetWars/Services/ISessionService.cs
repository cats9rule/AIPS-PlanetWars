using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.DTOs;
using PlanetWars.Data.Models;

namespace PlanetWars.Services
{
    public interface ISessionService
    {
        public Task<SessionDto> CreateSession( CreateGameDto dto);
        public Task<bool> Add(Session session);
        public Task<IEnumerable<SessionDto>> GetAllSessions();
        public Task<SessionDto> GetById(Guid id);
        public Task<IEnumerable<SessionDto>> GetByName(string name);
        public Task<bool> Update(UpdateSessionDto sessionDto);
        public Task<bool> Delete(Guid id);
        public Task<PlayerDto> AddPlayer(Guid sessionID, PlayerDto player);
    } 
}