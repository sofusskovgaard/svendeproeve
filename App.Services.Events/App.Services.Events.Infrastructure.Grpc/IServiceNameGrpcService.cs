using App.Services.Events.Infrastructure.Grpc.CommandMessages;
using App.Services.Events.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Events.Infrastructure.Grpc
{
    [Service]
    public interface IServiceNameGrpcService
    {
        [Operation]
        ValueTask<GetByIdGrpcCommandResult> GetById(GetByIdGrpcCommandMessage message);
    }
}