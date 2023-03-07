using App.Services.Organizations.Common.Dtos;
using App.Services.Organizations.Data.Entities;
using AutoMapper;

namespace App.Services.Organizations.Infrastructure.Mappers;

public class OrganizationEntityMapper : Profile
{
    public OrganizationEntityMapper()
    {
        CreateMap<OrganizationEntity, OrganizationDto>();
        CreateMap<OrganizationEntity, OrganizationDetailedDto>();
    }
}