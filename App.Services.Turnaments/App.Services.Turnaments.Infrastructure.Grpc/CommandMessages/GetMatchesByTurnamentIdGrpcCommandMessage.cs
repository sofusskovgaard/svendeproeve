using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetMatchesByTurnamentIdGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string TurnamentId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}