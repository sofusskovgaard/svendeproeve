using App.Services.Orders.Common.Test;
using App.Services.Orders.Data.Entities;
using AutoMapper;

namespace App.Services.Orders.Infrastructure.Mappers
{
    public class TestEntityMapper : Profile
    {
        public TestEntityMapper()
        {
            this.CreateMap<TestEntity, TestDto>()
                .Include<TestEntity, TestDetailedDto>();
        }
    }
}