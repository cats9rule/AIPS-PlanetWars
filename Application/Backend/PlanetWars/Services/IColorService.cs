using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.DTOs;

namespace PlanetWars.Services
{
    public interface IColorService
    {
        public Task<bool> Add(ColorDto colorDto);
        public Task<IEnumerable<ColorDto>> GetAll();
        public Task<ColorDto> GetById(Guid id);
        public Task<ColorDto> GetByHexValue(string hexValue);
        public Task<bool> Update(ColorDto colorDto);
        public Task<bool> Delete(Guid id);
    }
}