using App.Services.Orders.Common.Dtos;
using App.Services.Orders.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Orders.Infrastructure.Mappers
{
    public class ProductEntityMapper : Profile
    {
        public ProductEntityMapper()
        {
            this.CreateMap<ProductEntity, ProductDto>();
        }
    }
}
