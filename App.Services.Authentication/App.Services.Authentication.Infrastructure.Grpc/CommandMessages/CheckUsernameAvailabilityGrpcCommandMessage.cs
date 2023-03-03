using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class CheckUsernameAvailabilityGrpcCommandMessage : IGrpcCommandMessage
{
    [ProtoMember(1)]
    public string Username { get; set; }
}