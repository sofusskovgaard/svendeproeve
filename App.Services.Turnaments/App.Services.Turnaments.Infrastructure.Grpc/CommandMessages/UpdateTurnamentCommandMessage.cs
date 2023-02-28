using App.Infrastructure.Grpc;
using App.Services.Turnaments.Common.Dtos;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class UpdateTurnamentCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public TurnamentDto TurnamentDto { get; set; }
    }
}
