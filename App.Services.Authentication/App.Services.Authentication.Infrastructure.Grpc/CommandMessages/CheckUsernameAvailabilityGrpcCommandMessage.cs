using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Authentication.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class CheckUsernameAvailabilityGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string Username { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}