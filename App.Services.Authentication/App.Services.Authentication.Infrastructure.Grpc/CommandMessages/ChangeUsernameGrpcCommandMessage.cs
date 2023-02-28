using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class ChangeUsernameGrpcCommandMessage : IGrpcCommandMessage
{
    [ProtoMember(1)]
    public string UserId { get; set; }

    [ProtoMember(2)]
    public string Username { get; set; }
}