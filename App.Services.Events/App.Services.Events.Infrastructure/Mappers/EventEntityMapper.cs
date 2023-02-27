using App.Services.Events.Common.Dtos;
using App.Services.Events.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
