using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Events.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class CreateEventGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string EventName { get; set; }

    [ProtoMember(2)]
    public string Location { get; set; }

    [ProtoMember(3)]
    public DateTime StartDate { get; set; }

    [ProtoMember(4)]
    public DateTime EndDate { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}