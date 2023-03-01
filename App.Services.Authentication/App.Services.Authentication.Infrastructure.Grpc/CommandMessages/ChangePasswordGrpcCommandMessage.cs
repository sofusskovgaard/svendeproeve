using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class ChangePasswordGrpcCommandMessage : IGrpcCommandMessage
{
    [ProtoMember(1)]
    public string UserId { get; set; }

    [ProtoMember(2)]
    public string Password { get; set; }

    [ProtoMember(3)]
    public string ConfirmPassword { get; set; }
}