using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTurnamentByMatchIdGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string MatchId { get; set; }
    }
}
