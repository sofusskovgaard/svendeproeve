using App.Services.Tickets.Common.Test;
using App.Services.Tickets.Data.Entities;
using AutoMapper;

namespace App.Services.Tickets.Infrastructure.Mappers
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