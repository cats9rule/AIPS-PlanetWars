using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.ConcreteServices
{
    public class UserService : IUserService
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #region Helper Attributes
        private Random random;
        private const string alpha = "abcdefghijklmnopqrstuvwxyz";
        #endregion

        #endregion
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            random = new Random();
        }

        public async Task<UserDto> CreateUser(UserCreateDto userDto)
        {
            using (_unitOfWork)
            {
                User user = _mapper.Map<UserCreateDto, User>(userDto);
                user.Salt = GenerateSALT();
                string passwordSalt = user.Password + user.Salt;
                user.Password = HashPassword(passwordSalt);
                User someUser;
                String generatedTag;
                do
                {
                    generatedTag = random.Next(0, 9999).ToString("D4");
                    someUser = await _unitOfWork.Users.GetByUsernameAndTag(user.Username, generatedTag);
                } while (someUser != null);

                user.Tag = generatedTag;
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.CompleteAsync();
                return _mapper.Map<User, UserDto>(user);
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            using (_unitOfWork)
            {
                return _mapper.Map<List<UserDto>>(await _unitOfWork.Users.GetAll());
            }
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            using (_unitOfWork)
            {
                User user = await _unitOfWork.Users.GetById(id);
                if (user != null)
                {
                    return _mapper.Map<User, UserDto>(user);
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
                    return _mapper.Map<User, UserDto>(user);
                }
                return null;
            }
        }

        public async Task<bool> UpdateUser(UserDto userDto)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.Users.Update(_mapper.Map<UserDto, User>(userDto));
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.Users.Delete(id);
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<UserDto> LogInUser(UserLoginDto user)
        {
            using (_unitOfWork)
            {
                string username = user.Username;
                string tag = user.Tag;
                User u = await _unitOfWork.Users.GetByUsernameAndTag(username, tag);
                if (u == null)
                {
                    return null;
                }
                string passwordSalt = user.Password + u.Salt;
                string hashedPassword = this.HashPassword(passwordSalt);

                if (u.Password != hashedPassword)
                {
                    return null;
                }
                else
                {
                    return _mapper.Map<User, UserDto>(u);
                }
            }
        }

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