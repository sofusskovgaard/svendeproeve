using App.Services.Authentication.Common.Dtos;
using App.Services.Authentication.Data.Entities;
using AutoMapper;

namespace App.Services.Authentication.Infrastructure.Mappers;

public class UserSessionMapper : Profile
{
    public UserSessionMapper()
    {
        CreateMap<UserSessionEntity, UserSessionDto>().ReverseMap();
    }
}