using App.Common.Grpc;
using App.Services.Tournaments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Tournaments.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetMatchesGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    [ProtoMember(2)]
    public MatchDto[] Data { get; set; }
}