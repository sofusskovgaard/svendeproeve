using App.Common.Grpc;
using App.Services.Departments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class GetAllDepartmentsGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }
        [ProtoMember(2)]
        public IEnumerable<DepartmentDto> DepartmentDtos { get; set; }
    }
}
