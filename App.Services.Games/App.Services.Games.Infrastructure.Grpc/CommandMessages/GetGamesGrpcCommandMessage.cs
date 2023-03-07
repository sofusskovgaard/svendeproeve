using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetGamesGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string? SearchText { get; set; }

    [ProtoMember(2)]
    public string? Genre { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}