using App.Common.Grpc;
using App.Services.Turnaments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class UpdateMatchGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public MatchDto MatchDto { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}