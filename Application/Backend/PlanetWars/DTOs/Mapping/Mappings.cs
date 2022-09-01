using System;
using System.Linq;
using AutoMapper;
using PlanetWars.Data.Models;

namespace PlanetWars.DTOs.MappingProfiles
{
    public class Mappings : Profile
    {
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
            .ReverseMap();

            CreateMap<Player, PlayerDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src =>
                src.User.Username
                + "#"
                + src.User.Tag
                ))
            .ForMember(dest => dest.PlayerColor, opt => opt.MapFrom(src => src.PlayerColor.ColorHexValue))
            .ForMember(dest => dest.TurnIndex, opt => opt.MapFrom(dest => dest.PlayerColor.TurnIndex))
            .ForMember(dest => dest.Planets, opt => opt.MapFrom(src => src.Planets))
            .ReverseMap();

            CreateMap<PlayerColor, PlayerColorDto>()
            .ForMember(dest => dest.HexColor, opt => opt.MapFrom(src => src.ColorHexValue))
            .ReverseMap();

            // works
            CreateMap<Session, SessionDto>()
            .ReverseMap();

            CreateMap<GameMap, GameMapDto>()
            .ForMember(dest => dest.PlanetGraph, opt => opt.ConvertUsing(new PlanetGraphConverter(), src => src.PlanetGraph))
            .ForMember(dest => dest.PlanetMatrix, opt => opt.MapFrom(src => src.PlanetMatrix.ToArray<Char>().Select(c => c == '1' ? true : false).ToList()));

        }

    }
}