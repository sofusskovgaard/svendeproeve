using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTurnamentsByGameIdGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string GameId { get; set; }
    }
}
