using App.Services.Billing.Infrastructure.Grpc.CommandMessages;
using App.Services.Billing.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Billing.Infrastructure.Grpc
{
    [Service("app.services.billing")]
    public interface IBillingGrpcService
    {
        [Operation]
        ValueTask<GetBillingByIdGrpcCommandResult> GetBillingById(GetBillingByIdGrpcCommandMessage message);

        [Operation]
        ValueTask<CreateBillingGrpcCommandResult> CreateBilling(CreateBillingGrpcCommandMessage message);
    }
}