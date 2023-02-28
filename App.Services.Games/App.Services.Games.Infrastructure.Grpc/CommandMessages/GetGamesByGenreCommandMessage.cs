using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetGamesByGenreCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Genre { get; set; }
    }
}
