using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetUsersInOrganizationGrpcCommandMessage : IGrpcCommandMessage
{
    [ProtoMember(1)]
    public string OrganizatioId { get; set; }
}