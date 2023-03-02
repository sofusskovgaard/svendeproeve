using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetMatchesByTeamIdGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string TeamId { get; set; }
    }
}
