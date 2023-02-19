using ProtoBuf.Grpc.Configuration;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc.CommandResults;

namespace App.Services.Users.Infrastructure.Grpc;

[Service("users-service")]
public interface IUsersGrpcService
{
    [Operation]
    ValueTask<GetUserByIdCommandResult> GetUserById(GetUserByIdCommandMessage message);
}