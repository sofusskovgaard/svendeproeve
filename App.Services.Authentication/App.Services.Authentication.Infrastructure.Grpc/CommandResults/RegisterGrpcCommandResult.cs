using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class RegisterGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}