using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class RefreshTokenGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string RefreshToken { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}