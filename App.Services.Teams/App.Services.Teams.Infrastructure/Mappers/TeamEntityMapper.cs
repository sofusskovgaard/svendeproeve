using App.Services.Teams.Common.Dtos;
using App.Services.Teams.Data.Entities;
using AutoMapper;

namespace App.Services.Teams.Infrastructure.Mappers;

public class TeamEntityMapper : Profile
{
    public TeamEntityMapper()
    {
        CreateMap<TeamEntity, TeamDto>().ReverseMap();
        CreateMap<TeamEntity, UpdateTeamDto>().ReverseMap();
    }
}