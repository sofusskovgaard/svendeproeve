using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetTeamsByOrganizationIdGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string OrganizationId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}