using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetAllTurnamentsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}