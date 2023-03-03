using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetMatchesByTurnamentIdGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string TurnamentId { get; set; }
    }
}
