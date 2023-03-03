using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Turnaments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class DeleteMatchByIdCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }
    }
}
