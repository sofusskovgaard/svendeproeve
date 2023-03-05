using App.Services.RealTimeUpdater.Infrastructure.Grpc.CommandMessages;
using App.Services.RealTimeUpdater.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.RealTimeUpdater.Infrastructure.Grpc
{
    [Service("app.services.realtimeupdater")]
    public interface IServiceNameGrpcService
    {
        [Operation]
        ValueTask<GetByIdGrpcCommandResult> GetById(GetByIdGrpcCommandMessage message);
    }
}