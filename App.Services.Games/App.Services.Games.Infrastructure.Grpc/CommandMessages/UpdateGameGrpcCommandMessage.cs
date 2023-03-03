using App.Common.Grpc;
using App.Services.Games.Common.Dtos;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class UpdateGameGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public GameDto GameDto { get; set; }
    }
}
