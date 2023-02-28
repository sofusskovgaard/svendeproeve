using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class RefreshTokenGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    [ProtoMember(2)]
    public string AccessToken { get; set; }

    [ProtoMember(3)]
    public int ExpiresIn { get; set; }

    [ProtoMember(4)]
    public string Type { get; set; } = "Bearer";
}