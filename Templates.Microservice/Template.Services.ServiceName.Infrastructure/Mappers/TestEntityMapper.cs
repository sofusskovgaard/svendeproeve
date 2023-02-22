using AutoMapper;
using Template.Services.ServiceName.Common.Test;
using Template.Services.ServiceName.Data.Entities;

namespace Template.Services.ServiceName.Infrastructure.Mappers;

public class TestEntityMapper : Profile
{
    public TestEntityMapper()
    {
        this.CreateMap<TestEntity, TestDto>()
            .IncludeBase<TestEntity, TestDetailedDto>();
    }
}