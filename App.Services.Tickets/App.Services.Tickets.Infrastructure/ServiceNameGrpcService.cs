using App.Infrastructure.Grpc;
using App.Services.Tickets.Infrastructure.Grpc;
using App.Services.Tickets.Infrastructure.Grpc.CommandMessages;
using App.Services.Tickets.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Tickets.Infrastructure
{
    public class ServiceNameGrpcService : BaseGrpcService, IServiceNameGrpcService
    {
        public ValueTask<GetByIdGrpcCommandResult> GetById(GetByIdGrpcCommandMessage message) =>
            this.TryAsync(async () => new GetByIdGrpcCommandResult());
    }
}