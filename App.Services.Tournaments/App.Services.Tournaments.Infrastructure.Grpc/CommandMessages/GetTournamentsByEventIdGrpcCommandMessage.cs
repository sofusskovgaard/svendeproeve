using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Tournaments.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetTournamentsByEventIdGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string EventId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}