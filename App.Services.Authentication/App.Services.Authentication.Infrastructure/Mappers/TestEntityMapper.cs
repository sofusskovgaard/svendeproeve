using App.Services.Authentication.Common.Test;
using App.Services.Authentication.Data.Entities;
using AutoMapper;

namespace App.Services.Authentication.Infrastructure.Mappers
{
    public class TestEntityMapper : Profile
    {
        public TestEntityMapper()
        {
            this.CreateMap<UserLoginEntity, TestDto>()
                .Include<UserLoginEntity, TestDetailedDto>();
        }
    }
}