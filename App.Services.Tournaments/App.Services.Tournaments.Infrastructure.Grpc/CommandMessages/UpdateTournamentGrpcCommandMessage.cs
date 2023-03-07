using App.Common.Grpc;
using App.Services.Tournaments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Tournaments.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class UpdateTournamentGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public string GameId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}