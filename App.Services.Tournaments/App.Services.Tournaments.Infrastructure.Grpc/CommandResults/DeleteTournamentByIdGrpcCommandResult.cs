using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Tournaments.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class DeleteTournamentByIdGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}