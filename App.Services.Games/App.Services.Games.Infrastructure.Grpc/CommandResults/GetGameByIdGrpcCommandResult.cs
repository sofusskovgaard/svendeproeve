using App.Common.Grpc;
using App.Services.Games.Common.Dtos;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetGameByIdGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(2)]
    public GameDto GameDto { get; set; }

    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}