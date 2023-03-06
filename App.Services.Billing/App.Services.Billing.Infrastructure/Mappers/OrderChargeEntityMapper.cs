using App.Services.Billing.Common.Dtos;
using App.Services.Billing.Data.Entities;
using AutoMapper;

namespace App.Services.Billing.Infrastructure.Mappers;

public class OrderChargeEntityMapper : Profile
{
    public OrderChargeEntityMapper()
    {
        CreateMap<OrderChargeEntity, OrderChargeDto>().ReverseMap();
        CreateMap<OrderChargeEntity, OrderChargeDetailedDto>().ReverseMap();
    }
}