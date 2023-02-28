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
    public class OrderEntityMapper : Profile
    {
        public OrderEntityMapper() 
        {
            this.CreateMap<OrderEntity, OrderDto>();
        }
    }
}
