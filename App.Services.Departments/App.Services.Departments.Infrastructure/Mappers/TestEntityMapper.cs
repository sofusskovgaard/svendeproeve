using App.Services.Departments.Common.Test;
using App.Services.Departments.Data.Entities;
using AutoMapper;

namespace App.Services.Departments.Infrastructure.Mappers
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