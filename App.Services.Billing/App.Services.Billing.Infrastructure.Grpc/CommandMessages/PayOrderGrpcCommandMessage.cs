using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Billing.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class PayOrderGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string OrderId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}