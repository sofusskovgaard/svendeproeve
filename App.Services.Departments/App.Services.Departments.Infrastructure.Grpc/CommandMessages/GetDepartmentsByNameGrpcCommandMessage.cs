using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetDepartmentsByNameGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string Name { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}