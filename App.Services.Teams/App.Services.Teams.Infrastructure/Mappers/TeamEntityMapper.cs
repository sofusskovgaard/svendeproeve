using App.Services.Teams.Common.Dtos;
using App.Services.Teams.Data.Entities;
using AutoMapper;

namespace App.Services.Teams.Infrastructure.Mappers
{
    public class TeamEntityMapper : Profile
    {
        public TeamEntityMapper()
        {
            this.CreateMap<TeamEntity, TeamDto>().ReverseMap();
            this.CreateMap<TeamEntity, UpdateTeamDto>().ReverseMap();
        }
    }
}
