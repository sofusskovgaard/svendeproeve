using App.Common.Grpc;
using App.Services.Authentication.Common.Dtos;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class RefreshTokenGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    [ProtoMember(2)]
    public RefreshTokenDto? Data { get; set; }
}