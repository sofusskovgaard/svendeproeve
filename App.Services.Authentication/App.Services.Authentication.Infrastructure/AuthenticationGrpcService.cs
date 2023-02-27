using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Infrastructure.Options;
using App.Infrastructure.Utilities;
using App.Services.Authentication.Data.Entities;
using App.Services.Authentication.Infrastructure.Grpc;
using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Authentication.Infrastructure.Grpc.CommandResults;
using App.Services.Users.Infrastructure.Commands;
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
            return this.TryAsync(async () =>
            {
                var login = new UserLoginEntity()
                {
                    Username = message.Username,
                    Email = message.Email,
                };

                var passwordHashResponse = Hasher.Hash(message.Password);

                login.PasswordHash = passwordHashResponse.Hash;
                login.PasswordSalt = passwordHashResponse.Salt;

                await _entityDataService.SaveEntity(login);

                await _publishEndpoint.Publish(new CreateUserCommandMessage()
                {
                    Id = login.Id,
                    Firstname = message.Firstname,
                    Lastname = message.Lastname,
                    Username = login.Username,
                    Email = login.Email
                });

                return new RegisterGrpcCommandResult()
                {
                    Id = login.Id,
                    Username = login.Username,
                    Email = login.Email
                };
            });
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
                    // don't tell enduser if the user could be found or not, security by obscurity ;)
                    throw new Exception("Username or password is incorrect");
                }

                var valid = Hasher.VerifyValue(message.Password, login.PasswordHash, login.PasswordSalt);

                if (!valid)
                {
                    // more security by obscurity
                    throw new Exception("Username or password is incorrect");
                }

                var accessToken = JwtGenerator.GenerateAccessToken(new JwtPayload(login.Id, login.Username, login.Email));
                var refreshToken = JwtGenerator.GenerateRefreshToken();

                var refreshTokenHashResponse = Hasher.Hash(refreshToken, false);

                var session = new UserSessionEntity()
                {
                    TokenHash = refreshTokenHashResponse.Hash,
                    UserId = login.Id
                };

                await _entityDataService.SaveEntity(session);

                return new LoginGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata(),
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresIn = TimeSpan.FromSeconds(JwtOptions.TokenLifeTime).Seconds
                };
            });
        }
    }
}