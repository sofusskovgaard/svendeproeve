using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class DeleteOrganizationGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }
    }
}
