using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Events.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetEventsGrpcCommandMessage : IGrpcCommandMessage
    {
    }
}
