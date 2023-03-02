using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetAllTurnamentsGrpcCommandMessage : IGrpcCommandMessage
    {
    }
}
