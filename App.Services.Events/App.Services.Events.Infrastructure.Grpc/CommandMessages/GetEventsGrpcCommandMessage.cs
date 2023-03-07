using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Events.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetEventsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string? SearchText { get; set; }

    [ProtoMember(2)]
    public string? DepartmentId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}