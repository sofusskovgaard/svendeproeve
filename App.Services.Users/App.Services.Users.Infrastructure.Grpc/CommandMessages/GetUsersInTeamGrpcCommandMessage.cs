using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetUsersInTeamGrpcCommandMessage : IGrpcCommandMessage
{
    [ProtoMember(1)]
    public string TeamId { get; set; }
}