using App.Common.Grpc;
using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Departments.Common.Dtos;
using App.Services.Departments.Data.Entities;
using App.Services.Departments.Infrastructure.Commands;
using App.Services.Departments.Infrastructure.Grpc;
using App.Services.Departments.Infrastructure.Grpc.CommandMessages;
using App.Services.Departments.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Departments.Infrastructure;

public class DepartmentsGrpcService : BaseGrpcService, IDepartmentsGrpcService
{
    private readonly IEntityDataService _entityDataService;

    private readonly IMapper _mapper;

    private readonly IPublishEndpoint _publishEndpoint;

    public DepartmentsGrpcService(IEntityDataService entityDataService, IMapper mapper,
        IPublishEndpoint publishEndpoint)
    {
        this._entityDataService = entityDataService;
        this._mapper = mapper;
        this._publishEndpoint = publishEndpoint;
    }

    public ValueTask<GetAllDepartmentsGrpcCommandResult> GetAllDepartments(GetAllDepartmentsGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var departments = await this._entityDataService.ListEntities<DepartmentEntity>();

            return new GetAllDepartmentsGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true,
                    Message = "Getting departments"
                },
                DepartmentDtos = this._mapper.Map<IEnumerable<DepartmentDto>>(departments)
            };
        });
    }

    public ValueTask<GetDepartmentsByNameGrpcCommandResult> GetDepartmentsByName(
        GetDepartmentsByNameGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var departments = await this._entityDataService.ListEntities(
                new ExpressionFilterDefinition<DepartmentEntity>(entity => entity.Name.Contains(message.Name)));

            return new GetDepartmentsByNameGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true,
                    Message = "Getting departments"
                },
                DepartmentDtos = this._mapper.Map<IEnumerable<DepartmentDto>>(departments)
            };
        });
    }

    public ValueTask<GetDepartmentsByOrganizationIdGrpcCommandResult> GetDepartmentsByOrganizationId(
        GetDepartmentsByOrganizationIdGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var departments = await this._entityDataService.ListEntities(
                new ExpressionFilterDefinition<DepartmentEntity>(entity =>
                    entity.OrganizationIds.Contains(message.OrganizationId)));

            return new GetDepartmentsByOrganizationIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true,
                    Message = "Getting departments"
                },
                DepartmentDtos = this._mapper.Map<IEnumerable<DepartmentDto>>(departments)
            };
        });
    }

    public ValueTask<GetDepartmentByIdGrpcCommandResult> GetDepartmentById(GetDepartmentByIdGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var department = await this._entityDataService.GetEntity<DepartmentEntity>(message.Id);

            return new GetDepartmentByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true,
                    Message = "Getting department"
                },
                DepartmentDto = this._mapper.Map<DepartmentDto>(department)
            };
        });
    }

    public ValueTask<CreateDepartmentGrpcCommandResult> CreateDepartment(CreateDepartmentGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            await this._publishEndpoint.Publish(new CreateDepartmentCommandMessage
            {
                Name = message.Name,
                Address = message.Address
            });

            return new CreateDepartmentGrpcCommandResult
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
        return this.TryAsync(async () =>
        {
            await this._publishEndpoint.Publish(new UpdateDepartmentCommandMessage
            {
                Id = message.DepartmentDto.Id,
                Name = message.DepartmentDto.Name,
                Address = message.DepartmentDto.Address
            });

            return new UpdateDepartmentGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true,
                    Message = "Department updated"
                }
            };
        });
    }

    public ValueTask<DeleteDepartmentByIdGrpcCommandResult> DeleteDepartmentById(
        DeleteDepartmentByIdGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            await this._publishEndpoint.Publish(new DeleteDepartmentCommandMessage
            {
                Id = message.Id
            });

            return new DeleteDepartmentByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                }
            };
        });
    }
}