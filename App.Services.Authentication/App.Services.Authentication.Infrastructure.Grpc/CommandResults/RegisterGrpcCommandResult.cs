using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class RegisterGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    [ProtoMember(2)]
    public string Id { get; set; }

    [ProtoMember(3)]
    public string Username { get; set; }

    [ProtoMember(4)]
    public string Email { get; set; }
}