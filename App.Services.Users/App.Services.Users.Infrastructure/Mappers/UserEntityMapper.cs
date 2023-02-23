using App.Services.Users.Common.Dtos;
using App.Services.Users.Data.Entities;
using AutoMapper;

namespace App.Services.Users.Infrastructure.Mappers;

public class UserEntityMapper : Profile
{
    public UserEntityMapper()
    {
        this.CreateMap<UserEntity, UserDto>()
            .Include<UserEntity, UserDetailedDto>();
    }
}