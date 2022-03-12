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

            CreateMap<Planet, PlanetDto>()
            .ForMember(dest => dest.NeighbourPlanets, opt => opt.MapFrom(src => src.NeighbourPlanets.Select(relation => relation.PlanetToID).ToList()))
            .ReverseMap();

            CreateMap<Player, PlayerDto>().ReverseMap();

            CreateMap<PlayerColor, PlayerColorDto>()
            .ForMember(dest => dest.HexColor, opt => opt.MapFrom(src=> src.ColorHexValue))
            .ReverseMap();

            // works
            CreateMap<Session, SessionDto>()
            .ForMember(dest => dest.GalaxyID, opt => opt.MapFrom(src => src.Galaxy.ID))
            .ForMember(dest => dest.PlayerIDs, opt => opt.MapFrom(src => src.Players.Select(p => p.ID).ToList<Guid>()))
            .ReverseMap();

            CreateMap<GameMap, GameMapDto>()
            .ForMember(dest => dest.PlanetGraph, opt => opt.ConvertUsing(new PlanetGraphConverter(), src => src.PlanetGraph));

        }
        
    }
}