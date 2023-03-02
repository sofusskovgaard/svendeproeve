using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class RefreshTokenGrpcCommandMessage : IGrpcCommandMessage
{
    [ProtoMember(1)]
    public string RefreshToken { get; set; }
}