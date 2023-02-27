using App.Services.Billing.Common.Test;
using App.Services.Billing.Data.Entities;
using AutoMapper;

namespace App.Services.Billing.Infrastructure.Mappers
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