using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Billing.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class PayOrderGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(2)]
    public string? CheckoutUrl { get; set; }

    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}