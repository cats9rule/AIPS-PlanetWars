using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.DTOs;

namespace PlanetWars.Services
{
    public interface IUserService
    {
        public Task<UserDto> CreateUser(UserDto user);
        public Task<UserDto> GetUser(Guid id);
        public Task<IEnumerable<UserDto>> GetAllUsers();
        public Task<UserDto> GetUserByUsername(string username);
        public Task<UserDto> GetUserByTag(string tag);
        
    }
}