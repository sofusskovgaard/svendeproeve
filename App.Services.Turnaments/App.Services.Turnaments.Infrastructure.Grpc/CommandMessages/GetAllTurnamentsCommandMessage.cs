using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetAllTurnamentsCommandMessage : IGrpcCommandMessage
    {
    }
}
