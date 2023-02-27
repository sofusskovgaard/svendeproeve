using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Authentication.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Authentication.Infrastructure.Grpc
{
    [Service]
    public interface IAuthenticationGrpcService
    {
        [Operation]
        ValueTask<RegisterGrpcCommandResult> Register(RegisterGrpcCommandMessage message);

        [Operation]
        ValueTask<LoginGrpcCommandResult> Login(LoginGrpcCommandMessage message);
    }
}