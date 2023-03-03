using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class CheckEmailAvailabilityGrpcCommandMessage : IGrpcCommandMessage
{
    [ProtoMember(1)]
    public string Email { get; set; }
}