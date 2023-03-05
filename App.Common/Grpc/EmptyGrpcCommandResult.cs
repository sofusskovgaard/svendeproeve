using ProtoBuf;

namespace App.Common.Grpc;

[ProtoContract]
public class EmptyGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}