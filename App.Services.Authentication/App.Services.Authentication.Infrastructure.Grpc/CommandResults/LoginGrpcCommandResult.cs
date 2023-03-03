using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class LoginGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    [ProtoMember(2)]
    public string AccessToken { get; set; }

    [ProtoMember(3)]
    public string RefreshToken { get; set; }

    [ProtoMember(4)]
    public int ExpiresIn { get; set; }

    [ProtoMember(5)]
    public string Type { get; set; }
}