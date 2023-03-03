using App.Common.Grpc;
using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Departments.Common.Dtos;
using App.Services.Departments.Data.Entities;
using App.Services.Departments.Infrastructure.Events;
using App.Services.Departments.Infrastructure.Grpc;
using App.Services.Departments.Infrastructure.Grpc.CommandMessages;
using App.Services.Departments.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Departments.Infrastructure
{
    public class DepartmentsGrpcService : BaseGrpcService, IDepartmentsGrpcService
    {
        private readonly IEntityDataService _entityDataService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public DepartmentsGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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
                    DepartmentDtos = this._mapper.Map<IEnumerable<DepartmentDto>>(departments)
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
                    DepartmentDtos = this._mapper.Map<IEnumerable<DepartmentDto>>(departments)
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
                    DepartmentDtos = this._mapper.Map<IEnumerable<DepartmentDto>>(departments)
                };
            });
        }

        public ValueTask<GetDepartmentByIdCommandResult> GetDepartmentById(GetDepartmentByIdCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var department = await this._entityDataService.GetEntity<DepartmentEntity>(message.Id);

                return new GetDepartmentByIdCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = true,
                        Message = "Getting department"
                    },
                    DepartmentDto = this._mapper.Map<DepartmentDto>(department)
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

        public ValueTask<UpdateDepartmentCommandResult> UpdateDepartment(UpdateDepartmentCommandMessage message)
        {
            return TryAsync(async () =>
            {
                DepartmentEntity department = this._mapper.Map<DepartmentEntity>(message.DepartmentDto);
                await this._entityDataService.Update<DepartmentEntity>(department);

                return new UpdateDepartmentCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = true,
                        Message = "Department updated"
                    }
                };
            });
        }

        public ValueTask<DeleteDepartmentByIdCommandResult> DeleteDepartmentById(DeleteDepartmentByIdCommandMessage message)
        {
            return TryAsync(async () =>
            {
                DepartmentEntity department = await this._entityDataService.GetEntity<DepartmentEntity>(message.Id);

                GrpcCommandResultMetadata metadata;

                if (department != null)
                {
                    var res = await _entityDataService.Delete<DepartmentEntity>(department);

                    await _publishEndpoint.Publish(new DepartmentDeletedEventMessage() { Id = message.Id });

                    metadata = new GrpcCommandResultMetadata()
                    {
                        Success = res
                    };
                }
                else
                {
                    metadata = new GrpcCommandResultMetadata()
                    {
                        Success = false,
                        Message = "Could not find any departments with that id"
                    };
                }

                return new DeleteDepartmentByIdCommandResult()
                {
                    Metadata = metadata
                };
            });
        }
    }
}