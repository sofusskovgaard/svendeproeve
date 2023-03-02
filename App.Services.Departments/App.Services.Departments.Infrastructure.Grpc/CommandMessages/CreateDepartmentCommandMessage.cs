using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class CreateDepartmentCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Address { get; set; }
        [ProtoMember(3)]
        public string[] OrganizationIds { get; set; }
    }
}
