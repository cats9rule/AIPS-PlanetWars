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
        public Task<SessionDto> GetByNameAndCode(string name, string code);
        public Task<bool> Update(UpdateSessionDto sessionDto);
        public Task<bool> Delete(Guid id);
        public Task<PlayerDto> AddPlayer(Guid sessionID, PlayerDto player);
        public Task<bool> LeaveGame(LeaveGameDto dto);
    } 
}