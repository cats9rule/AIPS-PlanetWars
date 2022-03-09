using System;
using System.Linq;
using AutoMapper;
using PlanetWars.Data.Models;

namespace PlanetWars.DTOs.MappingProfiles
{
    public class Mappings : Profile
    {
        //TODO: check all mappings if they work as expected
        public Mappings() 
        {
            // works
            CreateMap<User, UserDto>().ReverseMap();
            // works
            CreateMap<UserLoginDto, User>();
            // works
            CreateMap<UserCreateDto, User>();

            CreateMap<Galaxy, GalaxyDto>().ReverseMap();

            CreateMap<Planet, PlanetDto>().ReverseMap();

            CreateMap<Player, PlayerDto>().ReverseMap();

            CreateMap<PlayerColor, PlayerColorDto>().ReverseMap();

            // works
            CreateMap<Session, SessionDto>()
            .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.CreatorID))
            .ForMember(dest => dest.GalaxyID, opt => opt.MapFrom(src => src.Galaxy.ID))
            .ForMember(dest => dest.PlayerIDs, opt => opt.MapFrom(src => src.Players.Select(p => p.ID).ToList<Guid>()))
            .ReverseMap();

        }
        
    }
}