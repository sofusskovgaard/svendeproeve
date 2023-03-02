using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Billing.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class PayOrderGrpcCommandMessage : IGrpcCommandMessage
{
    [ProtoMember(1)]
    public string OrderId { get; set; }
}