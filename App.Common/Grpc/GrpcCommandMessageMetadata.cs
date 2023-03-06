using ProtoBuf;

namespace App.Common.Grpc;

[ProtoContract]
public class GrpcCommandMessageMetadata
{
    [ProtoMember(1)]
    public string UserId { get; set; }

    [ProtoMember(2)]
    public string? ConnectionId { get; set; }

    [ProtoMember(3)]
    public bool IsAdmin { get; set; }
}