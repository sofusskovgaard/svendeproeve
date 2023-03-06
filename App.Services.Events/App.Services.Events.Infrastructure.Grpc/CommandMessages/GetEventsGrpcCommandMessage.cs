using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Events.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetEventsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}