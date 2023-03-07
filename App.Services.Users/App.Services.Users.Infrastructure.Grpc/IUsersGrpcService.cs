using ProtoBuf.Grpc.Configuration;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc.CommandResults;

namespace App.Services.Users.Infrastructure.Grpc;

[Service("app.services.users")]
public interface IUsersGrpcService
{
    [Operation]
    ValueTask<GetUserByIdGrpcCommandResult> GetUserById(GetUserByIdGrpcCommandMessage message);

    [Operation]
    ValueTask<GetUsersGrpcCommandResult> GetUsers(GetUsersGrpcCommandMessage message);
}