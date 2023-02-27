using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTeamsByManagerIdCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string ManagerId { get; set; }
    }
}
