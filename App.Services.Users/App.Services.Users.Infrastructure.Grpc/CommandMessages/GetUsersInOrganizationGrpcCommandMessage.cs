using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetUsersInOrganizationGrpcCommandMessage : IGrpcCommandMessage
{
    [ProtoMember(1)]
    public string OrganizatioId { get; set; }
}