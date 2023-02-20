using ProtoBuf.Grpc.Configuration;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc.CommandResults;

namespace App.Services.Users.Infrastructure.Grpc;

[Service("app.services.users")]
public interface IUsersGrpcService
{
    [Operation]
    Task<GetUserByIdCommandResult> GetUserById(GetUserByIdCommandMessage message);
}