using ProtoBuf.Grpc.Configuration;
using RealTimeUpdater.Infrastructure.Grpc.CommandMessages;
using RealTimeUpdater.Infrastructure.Grpc.CommandResults;

namespace RealTimeUpdater.Infrastructure.Grpc
{
    [Service]
    public interface IServiceNameGrpcService
    {
        [Operation]
        ValueTask<GetByIdGrpcCommandResult> GetById(GetByIdGrpcCommandMessage message);
    }
}