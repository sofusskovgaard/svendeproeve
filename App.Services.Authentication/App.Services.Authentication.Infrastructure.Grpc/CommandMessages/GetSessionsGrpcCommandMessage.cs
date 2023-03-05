using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetSessionsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}