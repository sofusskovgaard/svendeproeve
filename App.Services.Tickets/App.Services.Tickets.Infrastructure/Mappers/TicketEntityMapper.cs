using App.Services.Tickets.Common.Dtos;
using App.Services.Tickets.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Tickets.Infrastructure.Mappers
{
    public class TicketEntityMapper : Profile
    {
        public TicketEntityMapper()
        {
            this.CreateMap<TicketEntity, TicketDto>();
        }
    }
}
