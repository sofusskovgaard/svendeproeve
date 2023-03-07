using App.Common.Grpc;
using App.Services.Tournaments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Tournaments.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetTournamentByMatchIdGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(2)]
    public TournamentDto Data { get; set; }

    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}