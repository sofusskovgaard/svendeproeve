using App.Common.Grpc;
using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Users.Common.Dtos;
using App.Services.Users.Data.Entities;
using App.Services.Users.Infrastructure.Commands;
using App.Services.Users.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Users.Infrastructure;

public class UsersGrpcService : BaseGrpcService, IUsersGrpcService
{
    private readonly IEntityDataService _entityDataService;

    private readonly IMapper _mapper;

    private readonly IPublishEndpoint _publishEndpoint;

    public UsersGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public ValueTask<GetUserByIdGrpcCommandResult> GetUserById(GetUserByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var entity = await _entityDataService.GetEntity<UserEntity>(message.Id);

            return new GetUserByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = _mapper.Map<UserDto>(entity)
            };
        });
    }

    public async ValueTask<GetUsersGrpcCommandResult> GetUsers(GetUsersGrpcCommandMessage message)
    {
        var filters = new List<FilterDefinition<UserEntity>>();

        if (!string.IsNullOrEmpty(message.SearchText))
        {
            filters.Add(new FilterDefinitionBuilder<UserEntity>().Text(message.SearchText));
        }

        if (!string.IsNullOrEmpty(message.OrganizationId))
        {
            filters.Add(new FilterDefinitionBuilder<UserEntity>().AnyEq(entity => entity.Organizations, message.OrganizationId));
        }

        if (!string.IsNullOrEmpty(message.TeamId))
        {
            filters.Add(new FilterDefinitionBuilder<UserEntity>().AnyEq(entity => entity.Teams, message.TeamId));
        }

        var entities = await _entityDataService.ListEntities<UserEntity>(filters.Any() ? filter => filter.And(filters) : null);

        return new GetUsersGrpcCommandResult
        {
            Metadata = new GrpcCommandResultMetadata{ Success = true },
            Data = this._mapper.Map<UserDto[]>(entities)
        };
    }
}