using App.Services.Games.Common.Dtos;
using App.Services.Games.Data.Entities;
using AutoMapper;

namespace App.Services.Games.Infrastructure.Mappers;

public class GamesEntityMapper : Profile
{
    public GamesEntityMapper()
    {
        this.CreateMap<GameEntity, GameDto>();
        this.CreateMap<GameDto, GameEntity>();
    }
}