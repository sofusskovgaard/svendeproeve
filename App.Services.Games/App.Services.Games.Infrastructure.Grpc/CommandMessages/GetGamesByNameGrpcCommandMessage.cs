using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetGamesByNameGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string Name { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}