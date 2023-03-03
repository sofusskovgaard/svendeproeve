using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetGamesByNameGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Name { get; set; }
    }
}
