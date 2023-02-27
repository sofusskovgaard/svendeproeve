using App.Services.Organizations.Common.Dtos;
using App.Services.Organizations.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Organizations.Infrastructure.Mappers
{
    public class OrganizationEntityMapper : Profile
    {
        public OrganizationEntityMapper() 
        {
            this.CreateMap<OrganizationEntity, OrganizationDto>();
            this.CreateMap<OrganizationEntity, OrganizationDetailedDto>();
        }
    }
}
