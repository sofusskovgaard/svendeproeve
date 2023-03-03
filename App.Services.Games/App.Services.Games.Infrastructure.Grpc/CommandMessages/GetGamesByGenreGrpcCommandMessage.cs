using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetGamesByGenreGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Genre { get; set; }
    }
}
