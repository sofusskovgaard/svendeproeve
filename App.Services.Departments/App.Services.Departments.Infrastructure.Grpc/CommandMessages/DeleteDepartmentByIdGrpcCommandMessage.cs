using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class DeleteDepartmentByIdGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }
    }
}
