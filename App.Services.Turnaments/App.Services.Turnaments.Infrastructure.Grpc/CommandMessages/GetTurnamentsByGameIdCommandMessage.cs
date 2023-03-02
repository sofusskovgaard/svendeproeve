using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTurnamentsByGameIdCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string GameId { get; set; }
    }
}
