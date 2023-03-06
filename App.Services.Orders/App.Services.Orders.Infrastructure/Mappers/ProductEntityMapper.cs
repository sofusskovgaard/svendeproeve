using App.Services.Orders.Common.Dtos;
using App.Services.Orders.Data.Entities;
using AutoMapper;

namespace App.Services.Orders.Infrastructure.Mappers;

public class ProductEntityMapper : Profile
{
    public ProductEntityMapper()
    {
        CreateMap<ProductEntity, ProductDto>();
    }
}