using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetUsersGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public override GrpcCommandMessageMetadata Metadata { get; set; }
}