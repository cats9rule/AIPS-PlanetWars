using AutoMapper;
using PlanetWars.Data.Models;

namespace PlanetWars.DTOs.MappingProfiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles() 
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}