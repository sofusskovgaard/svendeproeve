using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Infrastructure.Options;
using App.Infrastructure.Utilities;
using App.Services.Authentication.Data.Entities;
using App.Services.Authentication.Infrastructure.Commands;
using App.Services.Authentication.Infrastructure.Grpc;
using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Authentication.Infrastructure.Grpc.CommandResults;
using App.Services.Users.Infrastructure.Commands;
using MassTransit;
using MongoDB.Driver;
using ProtoBuf.Grpc.Configuration;
using RabbitMQ.Client;
using System.Text.RegularExpressions;
using App.Services.Authentication.Infrastructure.Validators;

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
                PasswordValidator.Validate(message.Password, message.ConfirmPassword);

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
                    Metadata = new GrpcCommandResultMetadata(),
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

                var login = await _entityDataService.GetEntity<UserLoginEntity>(filter =>
                    filter.Or(filter.Eq(entity => entity.Email, message.Username),
                        filter.Eq(entity => entity.Username, message.Username)));

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
                    ExpiresIn = JwtOptions.TokenLifeTime
                };
            });
        }

        public ValueTask<RefreshTokenGrpcCommandResult> RefreshToken(RefreshTokenGrpcCommandMessage message)
        {
            return this.TryAsync(async () =>
            {
                var hashResponse = Hasher.Hash(message.RefreshToken, false);

                var session = await _entityDataService.GetEntity<UserSessionEntity>(filter =>
                    filter.Eq(entity => entity.TokenHash, hashResponse.Hash));

                if (session != null)
                {
                    var user = await _entityDataService.GetEntity<UserLoginEntity>(session.UserId);
                    var accessToken = JwtGenerator.GenerateAccessToken(new JwtPayload(user.Id, user.Username, user.Email));

                    return new RefreshTokenGrpcCommandResult()
                    {
                        Metadata = new GrpcCommandResultMetadata(),
                        AccessToken = accessToken,
                        ExpiresIn = JwtOptions.TokenLifeTime
                    };
                }

                return new RefreshTokenGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = false,
                        Message = "There is no session with that refresh token"
                    }
                };
            });
        }

        public ValueTask<KillUserSessionsGrpcCommandResult> KillSessions(KillUserSessionsGrpcCommandMessage message)
        {
            return this.TryAsync(async () =>
            {
                if (string.IsNullOrEmpty(message.UserId))
                {
                    throw new Exception($"{nameof(message.UserId)} is required");
                }

                if (message.SessionId == null || message.SessionId.Length == 0)
                {
                    var sessions = await _entityDataService.ListEntities<UserSessionEntity>(filter =>
                        filter.Eq(entity => entity.UserId, message.UserId));

                    await _publishEndpoint.PublishBatch(sessions.Select(session => new KillUserSessionCommandMessage()
                        { UserId = message.UserId, SessionId = session.Id! }));

                    return new KillUserSessionsGrpcCommandResult()
                    {
                        Metadata = new GrpcCommandResultMetadata()
                    };
                }

                await _publishEndpoint.PublishBatch(message.SessionId.Select(sessionId => new KillUserSessionCommandMessage()
                    { UserId = message.UserId, SessionId = sessionId }));

                return new KillUserSessionsGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata()
                };
            });
        }

        public ValueTask<CheckUsernameAvailabilityGrpcCommandResult> CheckUsernameAvailability(CheckUsernameAvailabilityGrpcCommandMessage message)
        {
            return this.TryAsync(async () =>
            {
                var login = await _entityDataService.GetEntity<UserLoginEntity>(filter =>
                    filter.Eq(entity => entity.Username, message.Username));

                return new CheckUsernameAvailabilityGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = login == null,
                        Message = login != null ? "Username already exists" : null
                    }
                };
            });
        }

        public ValueTask<CheckEmailAvailabilityGrpcCommandResult> CheckEmailAvailability(CheckEmailAvailabilityGrpcCommandMessage message)
        {
            return this.TryAsync(async () =>
            {
                var login = await _entityDataService.GetEntity<UserLoginEntity>(filter =>
                    filter.Eq(entity => entity.Email, message.Email));

                return new CheckEmailAvailabilityGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = login == null,
                        Message = login != null ? "Username already exists" : null
                    }
                };
            });
        }

        public ValueTask<ChangeUsernameGrpcCommandResult> ChangeUsername(ChangeUsernameGrpcCommandMessage message)
        {
            return this.TryAsync(async () =>
            {
                var exists = await CheckUsernameAvailability(new CheckUsernameAvailabilityGrpcCommandMessage()
                    { Username = message.Username });

                if (!exists.Metadata.Success)
                {
                    return new ChangeUsernameGrpcCommandResult
                    {
                        Metadata = exists.Metadata
                    };
                }

                await _publishEndpoint.Publish(new ChangeUsernameCommandMessage { UserId = message.UserId, Username = message.Username });

                return new ChangeUsernameGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                };
            });
        }

        public ValueTask<ChangeEmailGrpcCommandResult> ChangeEmail(ChangeEmailGrpCommandMessage message)
        {
            return this.TryAsync(async () =>
            {
                var exists = await CheckEmailAvailability(new CheckEmailAvailabilityGrpcCommandMessage()
                    { Email = message.Email });

                if (!exists.Metadata.Success)
                {
                    return new ChangeEmailGrpcCommandResult
                    {
                        Metadata = exists.Metadata
                    };
                }

                await _publishEndpoint.Publish(new ChangeEmailCommandMessage() { Email = message.Email });

                return new ChangeEmailGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                };
            });
        }

        public ValueTask<ChangePasswordGrpcCommandResult> ChangePassword(ChangePasswordGrpcCommandMessage message)
        {
            return this.TryAsync(async () =>
            {
                PasswordValidator.Validate(message.Password, message.ConfirmPassword);

                await _publishEndpoint.Publish(new ChangePasswordCommandMessage { Password = message.Password });

                return new ChangePasswordGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                };
            });
        }
    }
}