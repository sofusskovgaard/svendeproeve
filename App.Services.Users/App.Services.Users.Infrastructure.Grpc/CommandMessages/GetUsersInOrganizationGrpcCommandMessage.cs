using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetUsersInOrganizationGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string OrganizatioId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}