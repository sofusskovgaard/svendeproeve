using App.Infrastructure.Grpc;
using App.Services.Orders.Infrastructure.Grpc;
using App.Services.Orders.Infrastructure.Grpc.CommandMessages;
using App.Services.Orders.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Orders.Infrastructure
{
    public class ServiceNameGrpcService : BaseGrpcService, IServiceNameGrpcService
    {
        public ValueTask<GetByIdGrpcCommandResult> GetById(GetByIdGrpcCommandMessage message) =>
            this.TryAsync(async () => new GetByIdGrpcCommandResult());
    }
}