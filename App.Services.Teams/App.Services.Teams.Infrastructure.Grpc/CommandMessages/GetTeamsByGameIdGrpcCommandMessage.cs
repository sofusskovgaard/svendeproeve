using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTeamsByGameIdGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string GameId { get; set; }
    }
}
