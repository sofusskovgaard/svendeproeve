using ProtoBuf;

namespace App.Infrastructure.Grpc;

[ProtoContract]
public class GrpcCommandResultMetadata
{
    [ProtoMember(1)]
    public bool Success { get; set; } = true;

    [ProtoMember(2)]
    public string Message { get; set; }

    [ProtoMember(3)]
    public string[] Errors { get; set; }
}