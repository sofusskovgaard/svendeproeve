using App.Services.Billing.Common.Dtos;
using App.Services.Billing.Data.Entities;
using AutoMapper;

namespace App.Services.Billing.Infrastructure.Mappers;

public class UserCardEntityMapper : Profile
{
    public UserCardEntityMapper()
    {
        CreateMap<UserCardEntity, UserCardDto>().ReverseMap();
        CreateMap<UserCardEntity, UserCardDetailedDto>().ReverseMap();
    }
}