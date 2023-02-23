using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Infrastructure.Utilities;
using App.Services.Users.Common.Dtos;
using App.Services.Users.Data.Entities;
using App.Services.Users.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc.CommandResults;
using AutoMapper;

namespace App.Services.Users.Infrastructure;

public class UsersGrpcService : IUsersGrpcService
{
    private readonly IEntityDataService _entityDataService;

    private readonly IMapper _mapper;

    public UsersGrpcService(IEntityDataService entityDataService, IMapper mapper)
    {
        this._entityDataService = entityDataService;
        _mapper = mapper;
    }

    public async Task<GetUserByIdCommandResult> GetUserById(GetUserByIdCommandMessage message)
    {
        try
        {
            var user = await this._entityDataService.GetEntity<UserEntity>(message.Id);

            var dto = _mapper.Map<UserDto>(user);

            return new GetUserByIdCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Oh my lord it worked"
                },
                User = dto
            };
        }
        catch (Exception ex)
        {
            return new GetUserByIdCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata()
                {
                    Success = true,
                    Message = "Oh my lord it worked"
                }
            };
        }
    }

    public async Task<CreateUserCommandResult> CreateUser(CreateUserCommandMessage message)
    {
        var user = new UserEntity
        {
            Firstname = message.Firstname,
            Lastname = message.Lastname,
            Email = message.Email
        };

        var passwordHash = PasswordHasher.Hash(message.Password);

        user.PasswordHash = passwordHash.Hash;
        user.PasswordSalt = passwordHash.Salt;

        try
        {
            await _entityDataService.SaveEntity(user);

            return new CreateUserCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                User = _mapper.Map<UserDetailedDto>(user)
            };
        }
        catch (Exception ex)
        {
            var response = new CreateUserCommandResult()
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = false,
                    Message = ex.Message
                }
            };

            if (ex is AggregateException aex)
            {
                response.Metadata.Errors = aex.InnerExceptions.Select(e => e.Message).ToArray();
            }

            return response;
        }
    }

    private async Task<T> TryAsync<T>(Func<Task<T>> func) where T : IGrpcCommandResult
    {
        try
        {
            return await func.Invoke();
        }
        catch (AggregateException ex)
        {
            var response = Activator.CreateInstance<T>();

            response.Metadata = new GrpcCommandResultMetadata()
            {
                Success = false,
                Message = ex.Message,
                Errors = ex.InnerExceptions.Select(e => e.Message).ToArray()
            };

            return response;
        }
        catch (Exception ex)
        {
            var response = Activator.CreateInstance<T>();

            response.Metadata = new GrpcCommandResultMetadata()
            {
                Success = false,
                Message = ex.Message
            };

            return response;
        }
    }
}