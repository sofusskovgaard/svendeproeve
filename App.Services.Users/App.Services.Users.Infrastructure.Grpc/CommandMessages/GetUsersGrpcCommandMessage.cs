using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetUsersGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string? SearchText { get; set; }

    [ProtoMember(2)]
    public string? TeamId { get; set; }

    [ProtoMember(3)]
    public string? OrganizationId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata Metadata { get; set; }
}