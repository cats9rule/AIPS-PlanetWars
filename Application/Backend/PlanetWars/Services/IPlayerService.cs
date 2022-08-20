using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.DTOs;
using PlanetWars.Data.Models;

//CreatePlayer vraca Task<Player> 

namespace PlanetWars.Services
{
    public interface IPlayerService
    {
        // public Task<bool> Add(PlayerDto playerDto);
        public Task<IEnumerable<PlayerDto>> GetAll();
        public Task<PlayerDto> GetById(Guid id);
        public Task<IEnumerable<PlayerDto>> GetByUsernameAndTag(string username, string tag);
        public Task<IEnumerable<PlayerDto>> GetByUserId(Guid id);
        public Task<bool> Update(UpdatePlayerDto playerDto);
        public Task<bool> Delete(Guid id);
        public Task<PlayerDto> CreatePlayer(Guid userId, int turnIndex, Guid sessionId);
        public Task<SessionDto> SpawnPlayer(Guid sessionID, Guid playerID);
    }
}