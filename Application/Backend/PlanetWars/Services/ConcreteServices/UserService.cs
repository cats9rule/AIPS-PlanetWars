using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.ConcreteServices
{
    public class UserService : IUserService
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;

        #region Helper Attributes
        private Random random;
        private const string alpha = "abcdefghijklmnopqrstuvwxyz";
        #endregion

        #endregion
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            random = new Random();
        }

        public Task<UserDto> CreateUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            using (_unitOfWork)
            {
                IEnumerable<User> users = await _unitOfWork.Users.GetAll();
                List<UserDto> userDtos = new List<UserDto>();
                foreach (User user in users)
                {
                    userDtos.Add(ModelToDto(user));
                }
                return userDtos;
            }
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            using (_unitOfWork)
            {
                User user = await _unitOfWork.Users.GetById(id);
                if (user != null)
                {
                    return ModelToDto(user);
                }
                return null;
            }
        }

        public async Task<UserDto> GetUserByTag(string tag)
        {
            using (_unitOfWork)
            {
                User user = await _unitOfWork.Users.GetByTag(tag);
                if (user != null)
                {
                    return ModelToDto(user);
                }
                return null;
            }
        }

        public async Task<UserDto> GetUserByUsername(string username)
        {
            using (_unitOfWork)
            {
                User user = await _unitOfWork.Users.GetByUsername(username);
                if (user != null)
                {
                    return ModelToDto(user);
                }
                return null;
            }
        }

        public async Task<UserDto> GetUserByUsernameAndTag(string username, string tag)
        {
            using (_unitOfWork)
            {
                User user = await _unitOfWork.Users.GetByUsernameAndTag(username, tag);
                if (user != null)
                {
                    return ModelToDto(user);
                }
                return null;
            }
        }

        public async Task<bool> UpdateUser(UserDto user)
        {
            using (_unitOfWork)
            {
                return await _unitOfWork.Users.Update(DtoToModel(user));
            }
        }
        
        public async Task<bool> DeleteUser(Guid id)
        {
            using (_unitOfWork)
            {
                return await _unitOfWork.Users.Delete(id);
            }
        }

        #region Mappers
        private UserDto ModelToDto(User model)
        {
            return new UserDto
            {
                ID = model.ID,
                Username = model.Username,
                Tag = model.Tag,
                DisplayedName = model.DisplayedName
            };
        }
        private User DtoToModel(UserDto dto)
        {
            return new User
            {
                ID = dto.ID,
                Username = dto.Username,
                Tag = dto.Tag,
                DisplayedName = dto.DisplayedName
            };
        }
        #endregion

        #region Helpers

        private char GetRandomUppercase()
        {
            return alpha[random.Next(0, 26)];
        }
        private char GetRandomLowercase()
        {
            return Char.ToUpper(alpha[random.Next(0, 26)]);
        }
        private string GenerateSALT()
        {
            string salt = "";
            for (int i = 0; i < 12; i++)
            {
                if (i % 3 == 0)
                    salt += GetRandomUppercase();
                else if (i % 3 == 1)
                    salt += GetRandomLowercase();
                else
                    salt += random.Next(0, 10).ToString();
            }
            return salt;
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var hashedPassword = new System.Text.StringBuilder();
                var bytePassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                foreach (var b in bytePassword)
                {
                    hashedPassword.Append(b.ToString("x2"));
                }
                return hashedPassword.ToString();
            }
        }

        #endregion
    }
}