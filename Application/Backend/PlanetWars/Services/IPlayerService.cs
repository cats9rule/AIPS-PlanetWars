using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.DTOs;

namespace PlanetWars.Services
{
    public interface IPlayerService
    {
        public Task<bool> Add(PlayerDto playerDto);

        public Task<IEnumerable<PlayerDto>> GetAll();
        public Task<PlayerDto> GetById(Guid id);
        public Task<PlayerDto> GetByUsernameAndTag(string username, string tag);
        public Task<PlayerDto> GetByUserId(Guid id);

        public Task<bool> Update(PlayerDto playerDto);

        public Task<bool> Delete(Guid id);
    }
}