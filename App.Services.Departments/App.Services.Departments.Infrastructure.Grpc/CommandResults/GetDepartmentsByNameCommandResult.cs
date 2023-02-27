using App.Infrastructure.Grpc;
using App.Services.Departments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class GetDepartmentsByNameCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }
        [ProtoMember(2)]
        public IEnumerable<DepartmentDto> DepartmentDtos { get; set; }
    }
}
