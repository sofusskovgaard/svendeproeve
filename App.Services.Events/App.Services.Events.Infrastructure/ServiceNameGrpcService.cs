using App.Infrastructure.Grpc;
using App.Services.Events.Infrastructure.Grpc;
using App.Services.Events.Infrastructure.Grpc.CommandMessages;
using App.Services.Events.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Events.Infrastructure
{
    public class ServiceNameGrpcService : BaseGrpcService, IServiceNameGrpcService
    {
        public ValueTask<GetByIdGrpcCommandResult> GetById(GetByIdGrpcCommandMessage message) =>
            this.TryAsync(async () => new GetByIdGrpcCommandResult());
    }
}