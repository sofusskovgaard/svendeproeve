using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Infrastructure.Options;
using App.Services.Authentication.Data.Entities;
using App.Services.Authentication.Infrastructure.Commands;
using App.Services.Authentication.Infrastructure.Grpc;
using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Authentication.Infrastructure.Grpc.CommandResults;
using App.Services.Users.Infrastructure.Commands;
using MassTransit;
using App.Common.Grpc;
using App.Services.Authentication.Common.Dtos;
using App.Services.Authentication.Infrastructure.Services;
using App.Services.Authentication.Infrastructure.Validators;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace App.Services.Authentication.Infrastructure
{
    public class AuthenticationGrpcService : BaseGrpcService, IAuthenticationGrpcService
    {
        private readonly IEntityDataService _entityDataService;

        private readonly IPublishEndpoint _publishEndpoint;

        private readonly IJwtGeneratorService _jwtGeneratorService;

        private readonly IJwtKeyService _jwtKeyService;

        private readonly IMapper _mapper;

        public AuthenticationGrpcService(IPublishEndpoint publishEndpoint, IEntityDataService entityDataService, IJwtGeneratorService jwtGeneratorService, IJwtKeyService jwtKeyService, IMapper mapper)
        {
            _publishEndpoint = publishEndpoint;
            _entityDataService = entityDataService;
            _jwtGeneratorService = jwtGeneratorService;
            _jwtKeyService = jwtKeyService;
            this._mapper = mapper;
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
                    Email = login.Email,
                    ConnectionId = message.Metadata?.ConnectionId
                });

                return new RegisterGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
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

                var refreshToken = _jwtGeneratorService.GenerateRefreshToken();

                var refreshTokenHashResponse = Hasher.Hash(refreshToken, false);

                var session = new UserSessionEntity()
                {
                    TokenHash = refreshTokenHashResponse.Hash,
                    UserId = login.Id,
                    UserAgent = message.UserAgent,
                    IP = message.IP
                };

                await _entityDataService.SaveEntity(session);

                var accessToken = await _jwtGeneratorService.GenerateAccessToken(new JwtPayload(session.Id, login.Id, login.Username, login.Email, login.IsAdmin ?? false));

                return new LoginGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata{ Success = true },
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
                    var login = await _entityDataService.GetEntity<UserLoginEntity>(session.UserId);
                    var accessToken = await _jwtGeneratorService.GenerateAccessToken(new JwtPayload(session.Id, login.Id, login.Username, login.Email, login.IsAdmin ?? false));

                    return new RefreshTokenGrpcCommandResult
                    {
                        Metadata = new GrpcCommandResultMetadata{ Success = true },
                        AccessToken = accessToken,
                        ExpiresIn = JwtOptions.TokenLifeTime
                    };
                }

                throw new Exception("There is no session with that refresh token");
            });
        }

        public ValueTask<GetSessionsGrpcCommandResult> GetSessions(GetSessionsGrpcCommandMessage message)
        {
            return this.TryAsync(async () =>
            {
                var entities = await this._entityDataService.ListEntities<UserSessionEntity>(filter => filter.Eq(entity => entity.UserId, message.Metadata!.UserId));

                return new GetSessionsGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata { Success = true },
                    Sessions = this._mapper.Map<IEnumerable<UserSessionDto>>(entities).ToArray()
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
                        Metadata = new GrpcCommandResultMetadata { Success = true },
                    };
                }

                await _publishEndpoint.PublishBatch(message.SessionId.Select(sessionId => new KillUserSessionCommandMessage()
                    { UserId = message.UserId, SessionId = sessionId }));

                return new KillUserSessionsGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata { Success = true },
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
                await CheckUsernameAvailability(new CheckUsernameAvailabilityGrpcCommandMessage{ Metadata = message.Metadata, Username = message.Username });

                await _publishEndpoint.Publish(new ChangeUsernameCommandMessage
                {
                    UserId = message.Metadata!.UserId,
                    Username = message.Username
                });

                return new ChangeUsernameGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata{ Success = true },
                };
            });
        }

        public ValueTask<ChangeEmailGrpcCommandResult> ChangeEmail(ChangeEmailGrpcCommandMessage message)
        {
            return this.TryAsync(async () =>
            {
                await CheckEmailAvailability(new CheckEmailAvailabilityGrpcCommandMessage{ Metadata = message.Metadata, Email = message.Email });

                await _publishEndpoint.Publish(new ChangeEmailCommandMessage{
                    UserId = message.Metadata!.UserId,
                    Email = message.Email
                });

                return new ChangeEmailGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata{ Success = true }
                };
            });
        }

        public ValueTask<ChangePasswordGrpcCommandResult> ChangePassword(ChangePasswordGrpcCommandMessage message)
        {
            return this.TryAsync(async () =>
            {
                PasswordValidator.Validate(message.Password, message.ConfirmPassword);

                await _publishEndpoint.Publish(new ChangePasswordCommandMessage
                {
                    UserId = message.Metadata!.UserId,
                    Password = message.Password
                });

                return new ChangePasswordGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata{ Success = true }
                };
            });
        }

        public ValueTask<GetPublicKeyGrpcCommandResult> PublicKey(GetPublicKeyGrpcCommandMessage message)
        {
            return this.TryAsync(async () =>
            {
                var key = await _jwtKeyService.GetKey();

                return new GetPublicKeyGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata{ Success = true },
                    PublicKey = Convert.ToBase64String(key.Item2.ExportSubjectPublicKeyInfo()),
                    KeyId = key.Item1
                };
            });
        }
    }
}