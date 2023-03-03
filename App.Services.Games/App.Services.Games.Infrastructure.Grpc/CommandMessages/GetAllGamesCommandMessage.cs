using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Games.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetAllGamesCommandMessage : IGrpcCommandMessage
    {

    }
}
