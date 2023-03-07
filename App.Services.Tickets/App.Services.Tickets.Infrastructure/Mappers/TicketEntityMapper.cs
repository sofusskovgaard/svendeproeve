using App.Services.Tickets.Common.Dtos;
using App.Services.Tickets.Data.Entities;
using AutoMapper;

namespace App.Services.Tickets.Infrastructure.Mappers;

public class TicketEntityMapper : Profile
{
    public TicketEntityMapper()
    {
        CreateMap<TicketEntity, TicketDto>();
    }
}