using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetDepartmentsByOrganizationIdGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
            public string OrganizationId { get; set; }
    }
}
