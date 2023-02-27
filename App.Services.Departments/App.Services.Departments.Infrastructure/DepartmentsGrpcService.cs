using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Departments.Common.Dtos;
using App.Services.Departments.Data.Entities;
using App.Services.Departments.Infrastructure.Grpc;
using App.Services.Departments.Infrastructure.Grpc.CommandMessages;
using App.Services.Departments.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using Grpc.Core;
using MongoDB.Driver;

namespace App.Services.Departments.Infrastructure
{
    public class DepartmentsGrpcService : BaseGrpcService, IDepartmentsGrpcService
    {
        private readonly IEntityDataService _entityDataService;
        private readonly IMapper _mapper;
        public DepartmentsGrpcService(IEntityDataService entityDataService, IMapper mapper)
        {
            _entityDataService = entityDataService;
            _mapper = mapper;
        }

        public ValueTask<GetAllDepartmentsCommandResult> GetAllDepartments(GetAllDepartmentsCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var departments = await _entityDataService.ListEntities<DepartmentEntity>();

                return new GetAllDepartmentsCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = true,
                        Message = "Getting departments"
                    },
                    DepartmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments)
                };
            });
        }

        public ValueTask<GetDepartmentsByNameCommandResult> GetDepartmentsByName(GetDepartmentsByNameCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var departments = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<DepartmentEntity>(entity => entity.Name.Contains(message.Name)));

                return new GetDepartmentsByNameCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = true,
                        Message = "Getting departments"
                    },
                    DepartmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments)
                };
            });
        }

        public ValueTask<GetDepartmentsByOrganizationIdCommandResult> GetDepartmentsByOrganizationId(GetDepartmentsByOrganizationIdCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var departments = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<DepartmentEntity>(entity => entity.OrganizationIds.Contains(message.OrganizationId)));

                return new GetDepartmentsByOrganizationIdCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = true,
                        Message = "Getting departments"
                    },
                    DepartmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments)
                };
            });
        }

        public ValueTask<CreateDepartmentCommandResult> CreateDepartment(CreateDepartmentCommandMessage message)
        {
            return TryAsync(async () =>
            {
                DepartmentEntity department = new DepartmentEntity()
                {
                    Name = message.Name,
                    Address = message.Address,
                    OrganizationIds = message.OrganizationIds
                };

                await this._entityDataService.Create<DepartmentEntity>(department);

                return new CreateDepartmentCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true,
                        Message = "Department created"
                    }
                };
            });
        }
    }
}