using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Infrastructure.Utilities;
using App.Services.Users.Common.Dtos;
using App.Services.Users.Data.Entities;
using App.Services.Users.Infrastructure.Commands;
using App.Services.Users.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
            var user = await _entityDataService.GetEntity<UserEntity>(message.Id);

            return new GetUserByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                User = _mapper.Map<UserDto>(user)
            };
        });
    }

    public async ValueTask<GetUsersGrpcCommandResult> GetUsers(GetUsersGrpcCommandMessage message)
    {
        var users = await this._entityDataService.ListEntities<UserEntity>();

        return new GetUsersGrpcCommandResult
        {
            Metadata = new GrpcCommandResultMetadata(),
            Users = this._mapper.Map<UserDto[]>(users)
        };
    }

    public async ValueTask<GetUsersInTeamGrpcCommandResult> GetUsersInTeam(GetUsersInTeamGrpcCommandMessage message)
    {
        var users = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<UserEntity>(entity => entity.Teams.Contains(message.TeamId)));

        return new GetUsersInTeamGrpcCommandResult
        {
            Metadata = new GrpcCommandResultMetadata(),
            Users = this._mapper.Map<UserDto[]>(users)
        };
    }

    public async ValueTask<GetUsersInOrganizationGrpcCommandResult> GetUsersInOrganization(GetUsersInOrganizationGrpcCommandMessage message)
    {
        var users = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<UserEntity>(entity => entity.Organizations.Contains(message.OrganizatioId)));

        return new GetUsersInOrganizationGrpcCommandResult
        {
            Metadata = new GrpcCommandResultMetadata(),
            Users = this._mapper.Map<UserDto[]>(users)
        };
    }

    public ValueTask<CreateUserGrpcCommandResult> CreateUser(CreateUserGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var user = new UserEntity
            {
                Firstname = message.Firstname,
                Lastname = message.Lastname,
                Email = message.Email,
                Username = message.Username
            };

            var passwordHash = PasswordHasher.Hash(message.Password);

            user.PasswordHash = passwordHash.Hash;
            user.PasswordSalt = passwordHash.Salt;

            await _entityDataService.SaveEntity(user);

            var dto = _mapper.Map<UserDetailedDto>(user);

            return new CreateUserGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                User = dto
            };
        });
    }

    public async ValueTask<TestCommandResult> Test()
    {
        await _publishEndpoint.Publish(new TestCommandMessage());

        return new TestCommandResult()
        {
            Metadata = new GrpcCommandResultMetadata()
            {
                Success = true
            }
        };
    }
}