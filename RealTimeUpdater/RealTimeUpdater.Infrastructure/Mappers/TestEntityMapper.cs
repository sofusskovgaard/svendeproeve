using AutoMapper;
using RealTimeUpdater.Common.Test;
using RealTimeUpdater.Data.Entities;

namespace RealTimeUpdater.Infrastructure.Mappers
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