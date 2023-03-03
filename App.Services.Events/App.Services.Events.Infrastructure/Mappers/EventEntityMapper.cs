using App.Services.Events.Common.Dtos;
using App.Services.Events.Data.Entities;
using AutoMapper;

namespace App.Services.Events.Infrastructure.Mappers
{
    public class EventEntityMapper : Profile
    {
        public EventEntityMapper()
        {
            this.CreateMap<EventEntity, EventDto>();
            this.CreateMap<EventEntity, EventDetailedDto>();
        }
    }
}
