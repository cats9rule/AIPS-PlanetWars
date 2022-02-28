using System.Linq;
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

            CreateMap<Session, SessionDto>()
            .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.CreatorID))
            .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players.Select(p => p.ID).ToList()))
            .ForMember(dest => dest.Galaxy, opt => opt.MapFrom(src => src.Galaxy.ID))
            .ReverseMap();

        }
        
    }
}