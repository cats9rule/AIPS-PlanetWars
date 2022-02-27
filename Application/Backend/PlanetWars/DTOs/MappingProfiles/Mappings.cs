using AutoMapper;
using PlanetWars.Data.Models;

namespace PlanetWars.DTOs.MappingProfiles
{
    public class Mappings : Profile
    {

        public Mappings() 
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();

            CreateMap<Galaxy, GalaxyDto>().ReverseMap();

            CreateMap<Planet, PlanetDto>().ReverseMap();

            CreateMap<Player, PlayerDto>().ReverseMap();

            CreateMap<PlayerColor, PlayerColorDto>().ReverseMap();

            CreateMap<Session, SessionDto>().ReverseMap();

        }
        
    }
}