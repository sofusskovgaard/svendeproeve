using App.Infrastructure.Grpc;
using App.Services.Billing.Infrastructure.Grpc;
using App.Services.Billing.Infrastructure.Grpc.CommandMessages;
using App.Services.Billing.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Billing.Infrastructure
{
    public class ServiceNameGrpcService : BaseGrpcService, IServiceNameGrpcService
    {
        public ValueTask<GetByIdGrpcCommandResult> GetById(GetByIdGrpcCommandMessage message) =>
            this.TryAsync(async () => new GetByIdGrpcCommandResult());
    }
}