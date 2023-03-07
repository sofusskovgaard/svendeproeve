using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Tournaments.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class CreateTournamentGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string Name { get; set; }

    [ProtoMember(2)]
    public string GameId { get; set; }

    [ProtoMember(3)]
    public string EventId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}