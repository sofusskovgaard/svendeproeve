using App.Common.Grpc;
using App.Services.Games.Common.Dtos;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetGamesByNameGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(2)]
    public IEnumerable<GameDto> Data { get; set; }

    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}