using App.Common.Grpc;
using App.Services.Games.Common.Dtos;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class UpdateGameGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public GameDto GameDto { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}