using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class UpdateOrganizationGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public string? Bio { get; set; }

    [ProtoMember(4)]
    public string? ProfilePicture { get; set; }

    [ProtoMember(5)]
    public string? CoverPicture { get; set; }

    [ProtoMember(6)]
    public string? Address { get; set; }

    [ProtoMember(7)]
    public string? DepartmentId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}