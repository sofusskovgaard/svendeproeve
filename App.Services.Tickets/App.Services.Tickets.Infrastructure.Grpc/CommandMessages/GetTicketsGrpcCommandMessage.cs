using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Tickets.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetTicketsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string? UserId { get; set; }

    [ProtoMember(2)]
    public string? OrderId { get; set; }

    [ProtoMember(3)]
    public string? Status { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}