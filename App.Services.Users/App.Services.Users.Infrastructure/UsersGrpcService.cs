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

    public ValueTask<GetUserByIdCommandResult> GetUserById(GetUserByIdCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var user = await _entityDataService.GetEntity<UserEntity>(message.Id);

            return new GetUserByIdCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                User = _mapper.Map<UserDto>(user)
            };
        });
    }

    public ValueTask<CreateUserCommandResult> CreateUser(CreateUserCommandMessage message)
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

            return new CreateUserCommandResult
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