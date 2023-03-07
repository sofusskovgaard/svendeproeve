using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Orders.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetProductsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string? ReferenceId { get; set; }

    [ProtoMember(2)]
    public string? ReferenceType { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}