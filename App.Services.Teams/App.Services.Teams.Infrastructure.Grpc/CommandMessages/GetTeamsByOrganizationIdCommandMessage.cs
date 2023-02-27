using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTeamsByOrganizationIdCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string OrganizationId { get; set; }
    }
}
