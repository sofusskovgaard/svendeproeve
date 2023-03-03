using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class CreateDepartmentCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }
    }
}
