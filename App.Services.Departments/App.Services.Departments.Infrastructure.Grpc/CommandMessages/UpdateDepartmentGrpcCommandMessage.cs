using App.Common.Grpc;
using App.Services.Departments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class UpdateDepartmentGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public string Address { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}