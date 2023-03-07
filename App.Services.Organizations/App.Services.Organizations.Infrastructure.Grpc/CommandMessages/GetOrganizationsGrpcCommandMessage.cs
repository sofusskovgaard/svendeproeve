using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetOrganizationsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string? SearchText { get; set; }

    [ProtoMember(2)]
    public string? MemberId { get; set; }

    [ProtoMember(3)]
    public string? DepartmentId { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}