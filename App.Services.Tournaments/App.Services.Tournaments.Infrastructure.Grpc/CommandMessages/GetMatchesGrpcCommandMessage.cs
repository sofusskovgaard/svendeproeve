using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Tournaments.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetMatchesGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string? TeamId { get; set; }

    [ProtoMember(2)]
    public string? TournamentId { get; set; }

    [ProtoMember(3)]
    public string? WinningTeamId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}