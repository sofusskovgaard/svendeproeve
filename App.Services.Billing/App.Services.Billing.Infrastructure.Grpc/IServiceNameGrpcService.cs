using App.Services.Billing.Infrastructure.Grpc.CommandMessages;
using App.Services.Billing.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Billing.Infrastructure.Grpc
{
    [Service]
    public interface IServiceNameGrpcService
    {
        [Operation]
        ValueTask<GetByIdGrpcCommandResult> GetById(GetByIdGrpcCommandMessage message);
    }
}