using App.Infrastructure.Grpc;
using RealTimeUpdater.Infrastructure.Grpc;
using RealTimeUpdater.Infrastructure.Grpc.CommandMessages;
using RealTimeUpdater.Infrastructure.Grpc.CommandResults;

namespace RealTimeUpdater.Infrastructure
{
    public class ServiceNameGrpcService : BaseGrpcService, IServiceNameGrpcService
    {
        public ValueTask<GetByIdGrpcCommandResult> GetById(GetByIdGrpcCommandMessage message) =>
            this.TryAsync(async () => new GetByIdGrpcCommandResult());
    }
}