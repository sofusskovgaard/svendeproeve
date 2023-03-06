using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Orders.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetOrdersGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string? UserId { get; set; }

    // TODO: Add more filtering options

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}