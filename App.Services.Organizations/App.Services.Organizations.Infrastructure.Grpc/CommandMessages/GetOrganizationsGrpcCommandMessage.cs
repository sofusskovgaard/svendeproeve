using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetOrganizationsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}