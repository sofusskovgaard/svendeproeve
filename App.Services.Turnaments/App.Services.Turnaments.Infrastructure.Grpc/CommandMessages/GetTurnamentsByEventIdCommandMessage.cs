using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetTurnamentsByEventIdCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string EventId { get; set; }
    }
}
