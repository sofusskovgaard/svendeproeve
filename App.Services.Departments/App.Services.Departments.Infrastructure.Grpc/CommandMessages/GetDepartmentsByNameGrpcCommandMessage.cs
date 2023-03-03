using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetDepartmentsByNameGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Name { get; set; }
    }
}
