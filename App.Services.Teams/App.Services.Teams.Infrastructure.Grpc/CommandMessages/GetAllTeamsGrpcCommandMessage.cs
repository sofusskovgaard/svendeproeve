using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetAllTeamsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string? SearchText { get; set; }

    [ProtoMember(2)]
    public string? GameId { get; set; }

    [ProtoMember(3)]
    public string? OrganizationId { get; set; }

    [ProtoMember(4)]
    public string? MemberId { get; set; }

    [ProtoMember(5)]
    public string? ManagerId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}