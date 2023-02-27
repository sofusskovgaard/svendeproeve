using App.Services.Events.Common.Test;
using App.Services.Events.Data.Entities;
using AutoMapper;

namespace App.Services.Events.Infrastructure.Mappers
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