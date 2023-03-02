using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class CreateMatchCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string[] TeamsId { get; set; }
        [ProtoMember(3)]
        public string TurnamentId { get; set; }
    }
}
