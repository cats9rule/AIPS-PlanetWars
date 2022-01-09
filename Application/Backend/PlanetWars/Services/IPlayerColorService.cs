using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.DTOs;

namespace PlanetWars.Services
{
    public interface IPlayerColorService
    {
        public Task<bool> Add(PlayerColorDto pcDto);
        public Task<IEnumerable<PlayerColorDto>> GetAll();
        public Task<PlayerColorDto> GetById(Guid id);
        public Task<bool> Update(PlayerColorDto pcDto);
        public Task<bool> Delete(Guid id);
    }
}