using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTeamsByOrganizationIdGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string OrganizationId { get; set; }
    }
}
