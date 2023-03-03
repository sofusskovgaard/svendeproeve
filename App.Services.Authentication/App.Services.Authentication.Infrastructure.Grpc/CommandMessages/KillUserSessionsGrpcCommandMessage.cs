using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class KillUserSessionsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string UserId { get; set; }

    [ProtoMember(2)]
    public string[]? SessionId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}