using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class CreateTurnamentGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string GameId { get; set; }
        [ProtoMember(3)]
        public string[] MatchesId { get; set; }
        [ProtoMember(4)]
        public string EventId { get; set; }
    }
}
