using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Events.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class DeleteEventGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }
    }
}
