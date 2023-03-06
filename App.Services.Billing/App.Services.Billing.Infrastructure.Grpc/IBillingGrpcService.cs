using App.Services.Billing.Infrastructure.Grpc.CommandMessages;
using App.Services.Billing.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Billing.Infrastructure.Grpc;

[Service("app.services.billing")]
public interface IBillingGrpcService
{
    [Operation]
    ValueTask<GetChargeByOrderGrpcCommandResult> GetChargeByOrder(GetChargeByOrderGrpcCommandMessage message);

    [Operation]
    ValueTask<GetCardsForUserGrpcCommandResult> GetCardsForUser(GetCardsForUserGrpcCommandMessage message);
}