using App.Services.Turnaments.Common.Dtos;
using App.Services.Turnaments.Data.Entities;
using AutoMapper;

namespace App.Services.Turnaments.Infrastructure.Mappers;

public class EntityMapper : Profile
{
    public EntityMapper()
    {
        CreateMap<TurnamentEntity, TurnamentDto>();
        CreateMap<TurnamentDto, TurnamentEntity>();
        CreateMap<MatchEntity, MatchDto>();
        CreateMap<MatchDto, MatchEntity>();
    }
}