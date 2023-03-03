using App.Common.Grpc;
using App.Services.Departments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class UpdateDepartmentCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public DepartmentDto DepartmentDto { get; set; }
    }
}
