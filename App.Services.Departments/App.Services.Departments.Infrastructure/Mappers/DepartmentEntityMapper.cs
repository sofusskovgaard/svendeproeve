using App.Services.Departments.Common.Dtos;
using App.Services.Departments.Data.Entities;
using AutoMapper;

namespace App.Services.Departments.Infrastructure.Mappers
{
    public class DepartmentEntityMapper : Profile
    {
        public DepartmentEntityMapper()
        {
            this.CreateMap<DepartmentEntity, DepartmentDto>();
            this.CreateMap<DepartmentDto, DepartmentEntity>();
        }
    }
}
