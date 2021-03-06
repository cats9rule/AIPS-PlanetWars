using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.DTOs;

namespace PlanetWars.Services
{
    public interface IUserService
    {
        public Task<UserDto> CreateUser(UserCreateDto user);
        public Task<UserDto> GetUser(Guid id);
        public Task<IEnumerable<UserDto>> GetAllUsers();
        public Task<UserDto> GetUserByUsernameAndTag(string username, string tag);

        public Task<bool> DeleteUser(Guid id);
        public Task<bool> UpdateUser(UserDto user);
        public Task<UserDto> LogInUser(UserLoginDto user);
        
    }
}