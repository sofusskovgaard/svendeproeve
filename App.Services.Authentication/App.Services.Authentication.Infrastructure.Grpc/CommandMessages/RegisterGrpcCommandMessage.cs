using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class RegisterGrpcCommandMessage : IGrpcCommandMessage
{
    [ProtoMember(1)]
    public string Firstname { get; set; }

    [ProtoMember(2)]
    public string Lastname { get; set; }

    [ProtoMember(3)]
    public string Username { get; set; }

    [ProtoMember(4)]
    public string Email { get; set; }

    [ProtoMember(5)]
    public string Password { get; set; }

    [ProtoMember(6)]
    public string ConfirmPassword { get; set; }
}