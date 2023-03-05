using App.Common.Grpc;
using App.Services.Departments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetAllDepartmentsGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(2)]
    public IEnumerable<DepartmentDto> Data { get; set; }

    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}