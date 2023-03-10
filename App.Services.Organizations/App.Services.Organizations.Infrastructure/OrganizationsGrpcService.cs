using App.Common.Grpc;
using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Organizations.Common.Dtos;
using App.Services.Organizations.Data.Entities;
using App.Services.Organizations.Infrastructure.Commands;
using App.Services.Organizations.Infrastructure.Grpc;
using App.Services.Organizations.Infrastructure.Grpc.CommandMessages;
using App.Services.Organizations.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace App.Services.Organizations.Infrastructure;

public class OrganizationsGrpcService : BaseGrpcService, IOrganizationsGrpcService
{
    private readonly IEntityDataService _entityDataService;

    private readonly IMapper _mapper;

    private readonly IPublishEndpoint _publishEndpoint;

    public OrganizationsGrpcService(IEntityDataService entityDataService, IMapper mapper,
        IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public ValueTask<GetOrganizationByIdGrpcCommandResult> GetOrganizationById(GetOrganizationByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            OrganizationEntity entity;

            try
            {
                ObjectId.Parse(message.Id);

                entity = await _entityDataService.GetEntity<OrganizationEntity>(message.Id);
            }
            catch (FormatException)
            {
                entity = await _entityDataService.GetEntity<OrganizationEntity>(filter =>
                    filter.Eq(organizationEntity => organizationEntity.Name, message.Id));
            }

            return new GetOrganizationByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = _mapper.Map<OrganizationDto>(entity)
            };
        });
    }

    public ValueTask<GetOrganizationsGrpcCommandResult> GetOrganizations(GetOrganizationsGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var filters = new List<FilterDefinition<OrganizationEntity>>();

            if (!string.IsNullOrEmpty(message.SearchText))
            {
                filters.Add(new FilterDefinitionBuilder<OrganizationEntity>().Text(message.SearchText));
            }

            if (!string.IsNullOrEmpty(message.MemberId))
            {
                filters.Add(new FilterDefinitionBuilder<OrganizationEntity>().AnyEq(entity => entity.MemberIds, message.MemberId));
            }

            if (!string.IsNullOrEmpty(message.DepartmentId))
            {
                filters.Add(new FilterDefinitionBuilder<OrganizationEntity>().Eq(entity => entity.DepartmentId, message.DepartmentId));
            }

            var entities = await _entityDataService.ListEntities<OrganizationEntity>(filter =>
                filters.Any() ? filter.And(filters) : FilterDefinition<OrganizationEntity>.Empty);

            return new GetOrganizationsGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = _mapper.Map<OrganizationDto[]>(entities)
            };
        });
    }

    public ValueTask<CreateOrganizationGrpcCommandResult> CreateOrganization(CreateOrganizationGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new CreateOrganizationCommandMessage
            {
                Address = message.Address,
                Bio = message.Bio,
                CoverPicture = message.CoverPicture,
                Name = message.Name,
                ProfilePicture = message.ProfilePicture,
                DepartmentId = message.DepartmentId
            });

            return new CreateOrganizationGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata{ Success = true } };
        });
    }

    public ValueTask<UpdateOrganizationGrpcCommandResult> UpdateOrganization(UpdateOrganizationGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new UpdateOrganizationCommandMessage
            {
                Id = message.Id,
                Address = message.Address,
                Bio = message.Bio,
                CoverPicture = message.CoverPicture,
                Name = message.Name,
                ProfilePicture = message.ProfilePicture,
                DepartmentId = message.DepartmentId
            });

            return new UpdateOrganizationGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata{ Success = true } };
        });
    }

    public ValueTask<DeleteOrganizationGrpcCommandResult> DeleteOrganization(DeleteOrganizationGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new DeleteOrganizationCommandMessage{ Id = message.Id });
            return new DeleteOrganizationGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata{ Success = true } };
        });
    }
}