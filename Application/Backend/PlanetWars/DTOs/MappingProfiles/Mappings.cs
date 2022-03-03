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

            CreateMap<UserCreateDto, User>();

            CreateMap<Galaxy, GalaxyDto>().ReverseMap();

            CreateMap<Planet, PlanetDto>().ReverseMap();

            CreateMap<Player, PlayerDto>().ReverseMap();

            CreateMap<PlayerColor, PlayerColorDto>().ReverseMap();

            //FIXME: this one is sus, either duplicates player in session or doesn't display them at all
            CreateMap<Session, SessionDto>()
            .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.CreatorID))
            .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players.ConvertAll<Guid>(p => p.ID)))
            .ForMember(dest => dest.Galaxy, opt => opt.MapFrom(src => src.Galaxy.ID))
            .ReverseMap();

        }
        
    }
}