using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Infrastructure.Utilities;
using App.Services.Authentication.Data.Entities;
using App.Services.Authentication.Infrastructure.Grpc;
using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Authentication.Infrastructure.Grpc.CommandResults;
using MassTransit;
using MongoDB.Driver;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Authentication.Infrastructure
{
    public class AuthenticationGrpcService : BaseGrpcService, IAuthenticationGrpcService
    {
        private readonly IEntityDataService _entityDataService;

        private readonly IPublishEndpoint _publishEndpoint;

        public AuthenticationGrpcService(IPublishEndpoint publishEndpoint, IEntityDataService entityDataService)
        {
            _publishEndpoint = publishEndpoint;
            _entityDataService = entityDataService;
        }

        public ValueTask<RegisterGrpcCommandResult> Register(RegisterGrpcCommandMessage message)
        {
            return this.TryAsync(async () => new RegisterGrpcCommandResult());
        }

        public ValueTask<LoginGrpcCommandResult> Login(LoginGrpcCommandMessage message)
        {
            return this.TryAsync(async () =>
            {
                if (string.IsNullOrEmpty(message.Password))
                {
                    throw new Exception($"{nameof(message.Password)} is required");
                }

                if (string.IsNullOrEmpty(message.Username))
                {
                    throw new Exception($"{nameof(message.Username)} is required");
                }

                var login = await _entityDataService.GetEntity(
                    new ExpressionFilterDefinition<UserLoginEntity>(entity =>
                        entity.Email == message.Username || entity.Username == message.Username));

                if (login == null)
                {
                    throw new Exception("User with that username does not exist");
                }

                var valid = Hasher.VerifyValue(message.Password, login.PasswordHash, login.PasswordSalt);

                if (!valid)
                {
                    throw new Exception($"{nameof(message.Password)} is incorrect");
                }

                var token = JwtGenerator.GenerateAccessToken(new JwtPayload(login.Id, login.Username, login.Email));

                return new LoginGrpcCommandResult();
            });
        }
    }
}