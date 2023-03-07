using App.Services.Tournaments.Common.Dtos;
using App.Services.Tournaments.Data.Entities;
using AutoMapper;

namespace App.Services.Tournaments.Infrastructure.Mappers;

public class EntityMapper : Profile
{
    public EntityMapper()
    {
        CreateMap<TournamentEntity, TournamentDto>();
        CreateMap<TournamentDto, TournamentEntity>();
        CreateMap<MatchEntity, MatchDto>();
        CreateMap<MatchDto, MatchEntity>();
    }
}