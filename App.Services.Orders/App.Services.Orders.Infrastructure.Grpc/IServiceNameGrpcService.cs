using App.Services.Orders.Infrastructure.Grpc.CommandMessages;
using App.Services.Orders.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Orders.Infrastructure.Grpc
{
    [Service]
    public interface IServiceNameGrpcService
    {
        [Operation]
        ValueTask<GetByIdGrpcCommandResult> GetById(GetByIdGrpcCommandMessage message);
    }
}