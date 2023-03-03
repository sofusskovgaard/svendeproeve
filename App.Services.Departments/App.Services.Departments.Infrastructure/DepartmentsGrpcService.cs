using App.Common.Grpc;
using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Departments.Common.Dtos;
using App.Services.Departments.Data.Entities;
using App.Services.Departments.Infrastructure.Commands;
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

        public ValueTask<GetAllDepartmentsGrpcCommandResult> GetAllDepartments(GetAllDepartmentsGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var departments = await _entityDataService.ListEntities<DepartmentEntity>();

                return new GetAllDepartmentsGrpcCommandResult()
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

        public ValueTask<GetDepartmentsByNameGrpcCommandResult> GetDepartmentsByName(GetDepartmentsByNameGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var departments = await _entityDataService.ListEntities(new ExpressionFilterDefinition<DepartmentEntity>(entity => entity.Name.Contains(message.Name)));

                return new GetDepartmentsByNameGrpcCommandResult()
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

        public ValueTask<GetDepartmentsByOrganizationIdGrpcCommandResult> GetDepartmentsByOrganizationId(GetDepartmentsByOrganizationIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var departments = await _entityDataService.ListEntities(new ExpressionFilterDefinition<DepartmentEntity>(entity => entity.OrganizationIds.Contains(message.OrganizationId)));

                return new GetDepartmentsByOrganizationIdGrpcCommandResult()
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

        public ValueTask<GetDepartmentByIdGrpcCommandResult> GetDepartmentById(GetDepartmentByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var department = await _entityDataService.GetEntity<DepartmentEntity>(message.Id);

                return new GetDepartmentByIdGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = true,
                        Message = "Getting department"
                    },
                    DepartmentDto = _mapper.Map<DepartmentDto>(department)
                };
            });
        }

        public ValueTask<CreateDepartmentGrpcCommandResult> CreateDepartment(CreateDepartmentGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                await _publishEndpoint.Publish(new CreateDepartmentCommandMessage
                {
                    Name = message.Name,
                    Address = message.Address
                });

                return new CreateDepartmentGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    }
                };
            });
        }

        public ValueTask<UpdateDepartmentGrpcCommandResult> UpdateDepartment(UpdateDepartmentGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                DepartmentEntity department = _mapper.Map<DepartmentEntity>(message.DepartmentDto);
                await _entityDataService.Update<DepartmentEntity>(department);

                return new UpdateDepartmentGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = true,
                        Message = "Department updated"
                    }
                };
            });
        }

        public ValueTask<DeleteDepartmentByIdGrpcCommandResult> DeleteDepartmentById(DeleteDepartmentByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                DepartmentEntity department = await _entityDataService.GetEntity<DepartmentEntity>(message.Id);

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

                return new DeleteDepartmentByIdGrpcCommandResult()
                {
                    Metadata = metadata
                };
            });
        }
    }
}