using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Tournaments.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class CreateMatchGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string Name { get; set; }

    [ProtoMember(2)]
    public string[] TeamsId { get; set; }

    [ProtoMember(3)]
    public string TournamentId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}