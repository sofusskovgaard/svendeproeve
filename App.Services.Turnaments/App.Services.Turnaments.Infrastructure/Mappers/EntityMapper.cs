using App.Services.Turnaments.Common.Dtos;
using App.Services.Turnaments.Data.Entities;
using AutoMapper;

namespace App.Services.Turnaments.Infrastructure.Mappers
{
    public class EntityMapper : Profile
    {
        public EntityMapper()
        {
            this.CreateMap<TurnamentEntity, TurnamentDto>();
            this.CreateMap<TurnamentDto, TurnamentEntity>();
            this.CreateMap<MatchEntity, MatchDto>();
            this.CreateMap<MatchDto, MatchEntity>();
        }
    }
}
