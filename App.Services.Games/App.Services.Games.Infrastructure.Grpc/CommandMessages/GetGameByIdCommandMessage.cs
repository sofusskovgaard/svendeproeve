using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetGameByIdCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }
    }
}
