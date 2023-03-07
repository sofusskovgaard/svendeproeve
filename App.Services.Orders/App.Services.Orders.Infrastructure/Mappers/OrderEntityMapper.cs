using App.Services.Orders.Common.Dtos;
using App.Services.Orders.Data.Entities;
using AutoMapper;

namespace App.Services.Orders.Infrastructure.Mappers;

public class OrderEntityMapper : Profile
{
    public OrderEntityMapper()
    {
        CreateMap<OrderEntity, OrderDto>();
    }
}